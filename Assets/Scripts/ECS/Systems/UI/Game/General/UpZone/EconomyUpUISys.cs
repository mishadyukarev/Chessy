using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.Workers.Game.Else.Economy;
using Leopotam.Ecs;
using System.Collections.Generic;

internal sealed class EconomyUpUISys : IEcsRunSystem
{
    private EcsFilter<EconomyViewUICom> _economyUIFilter = default;

    private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitsFilter = default;
    private EcsFilter<CellBuildDataComponent, OwnerCom> _cellBuildFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvDatFilt = default;

    private EcsFilter<InventResourCom> _amountResFilter = default;
    private EcsFilter<UpgradesBuildsCom> _upgradeBuildsFilter = default;


    public void Run()
    {
        ref var econViewUICom = ref _economyUIFilter.Get1(0);

        ref var amountResCom = ref _amountResFilter.Get1(0);
        ref var amountBuildUpgsCom = ref _upgradeBuildsFilter.Get1(0);

        var builds = new Dictionary<BuildingTypes, int>();
        builds.Add(BuildingTypes.Farm, default);
        builds.Add(BuildingTypes.Woodcutter, default);
        builds.Add(BuildingTypes.Mine, default);

        byte amountUnitsInGame = 0;
        byte amountAddWood = 0;

        foreach (var curIdxCell in _cellBuildFilter)
        {
            ref var curUnitDatCom = ref _cellUnitsFilter.Get1(curIdxCell);
            ref var curOnUnitCom = ref _cellUnitsFilter.Get2(curIdxCell);

            ref var curBuildDatCom = ref _cellBuildFilter.Get1(curIdxCell);
            ref var curOnBuildCom = ref _cellBuildFilter.Get2(curIdxCell);


            if (curUnitDatCom.HaveUnit)
            {
                if (curOnUnitCom.IsMine)
                {
                    if (!curUnitDatCom.IsUnit(UnitTypes.King)) ++amountUnitsInGame;

                    if (curUnitDatCom.IsUnit(UnitTypes.Pawn))
                    {
                        if (curUnitDatCom.IsCondType(CondUnitTypes.Relaxed))
                        {
                            if (_cellEnvDatFilt.Get1(curIdxCell).HaveEnvir(EnvirTypes.AdultForest))
                            {
                                amountAddWood += 1;
                            }
                        }
                    }
                }
            }

            if (curBuildDatCom.HaveBuild)
            {
                if (curOnBuildCom.IsMine)
                {
                    if (curBuildDatCom.IsBuildType(BuildingTypes.Farm))
                    {
                        ++builds[BuildingTypes.Farm];
                    }
                    else if (curBuildDatCom.IsBuildType(BuildingTypes.Woodcutter))
                    {
                        ++builds[BuildingTypes.Woodcutter];
                    }

                    else if (curBuildDatCom.IsBuildType(BuildingTypes.Mine))
                    {
                        ++builds[BuildingTypes.Mine];
                    }

                }
            }
        }
        var amountUpgsFarm = amountBuildUpgsCom.AmountUpgs(WhoseMoveCom.CurPlayer, BuildingTypes.Farm);
        var extractOneFarm = ExtractionInfoSupport.ExtractOneBuild(BuildingTypes.Farm, amountUpgsFarm);

        var amountAddFood = 1 + builds[BuildingTypes.Farm] * extractOneFarm - amountUnitsInGame;


        if (amountAddFood < 0) econViewUICom.SetAddText(ResourceTypes.Food, amountAddFood.ToString());

        else econViewUICom.SetAddText(ResourceTypes.Food, "+ " + amountAddFood.ToString());



        var amountUpgsWoodcut = amountBuildUpgsCom.AmountUpgs(WhoseMoveCom.CurPlayer, BuildingTypes.Woodcutter);
        amountAddWood += (byte)(builds[BuildingTypes.Woodcutter] * ExtractionInfoSupport.ExtractOneBuild(BuildingTypes.Woodcutter, amountUpgsWoodcut));

        econViewUICom.SetAddText(ResourceTypes.Wood, "+ " + amountAddWood);



        var amountUpgrsMine = amountBuildUpgsCom.AmountUpgs(WhoseMoveCom.CurPlayer, BuildingTypes.Mine);
        var amountAddOre = builds[BuildingTypes.Mine] * ExtractionInfoSupport.ExtractOneBuild(BuildingTypes.Mine, amountUpgrsMine);

        econViewUICom.SetAddText(ResourceTypes.Ore, "+ " + amountAddOre);




        econViewUICom.SetMainText(ResourceTypes.Food, amountResCom.AmountResources(WhoseMoveCom.CurPlayer, ResourceTypes.Food).ToString());
        econViewUICom.SetMainText(ResourceTypes.Wood, amountResCom.AmountResources(WhoseMoveCom.CurPlayer, ResourceTypes.Wood).ToString());
        econViewUICom.SetMainText(ResourceTypes.Ore, amountResCom.AmountResources(WhoseMoveCom.CurPlayer, ResourceTypes.Ore).ToString());
        econViewUICom.SetMainText(ResourceTypes.Iron, amountResCom.AmountResources(WhoseMoveCom.CurPlayer, ResourceTypes.Iron).ToString());
        econViewUICom.SetMainText(ResourceTypes.Gold, amountResCom.AmountResources(WhoseMoveCom.CurPlayer, ResourceTypes.Gold).ToString());

    }
}