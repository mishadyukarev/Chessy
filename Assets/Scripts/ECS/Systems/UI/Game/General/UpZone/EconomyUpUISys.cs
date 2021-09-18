using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.Workers.Game.Else.Economy;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class EconomyUpUISys : IEcsRunSystem
{
    private EcsFilter<EconomyViewUICom> _economyUIFilter = default;

    private EcsFilter<CellUnitDataCom, OwnerOnlineComp> _cellUnitsFilter = default;
    private EcsFilter<CellBuildDataComponent, OwnerOnlineComp> _cellBuildFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvDataFilter = default;

    private EcsFilter<InventorResourcesComponent> _amountResFilter = default;
    private EcsFilter<UpgradesBuildingsComponent> _upgradeBuildsFilter = default;


    public void Run()
    {
        ref var economyViewUICom = ref _economyUIFilter.Get1(0);

        ref var amountResCom = ref _amountResFilter.Get1(0);
        ref var amountBuildUpgradesCom = ref _upgradeBuildsFilter.Get1(0);

        byte amountFarm = 0;
        byte amountWoodcutter = 0;
        byte amountMine = 0;

        byte amountUnitsInGame = 0;


        byte amountAddingWood = 0;

        foreach (var curIdxCell in _cellBuildFilter)
        {
            ref var curCellUnitDataComp = ref _cellUnitsFilter.Get1(curIdxCell);
            ref var curOwnerCellUnitComp = ref _cellUnitsFilter.Get2(curIdxCell);

            ref var curCellBuildDataComp = ref _cellBuildFilter.Get1(curIdxCell);
            ref var curOwnerCellBuildComp = ref _cellBuildFilter.Get2(curIdxCell);


            if (curCellUnitDataComp.HaveUnit)
            {
                if (curOwnerCellUnitComp.HaveOwner)
                {
                    if (curOwnerCellUnitComp.IsMine)
                    {
                        if (!curCellUnitDataComp.Is(UnitTypes.King)) ++amountUnitsInGame;

                        if (curCellUnitDataComp.Is(UnitTypes.Pawn))
                        {
                            if (curCellUnitDataComp.IsConditionType(CondUnitTypes.Relaxed))
                            {
                                if (_cellEnvDataFilter.Get1(curIdxCell).HaveEnvironment(EnvironmentTypes.AdultForest))
                                {
                                    amountAddingWood += 1;
                                }
                            }
                        }
                    }
                }
            }

            if (curCellBuildDataComp.HaveBuild)
            {
                if (curOwnerCellBuildComp.HaveOwner)
                {
                    if (curOwnerCellBuildComp.IsMine)
                    {
                        if (curCellBuildDataComp.IsBuildType(BuildingTypes.Farm))
                        {
                            ++amountFarm;
                        }
                        else if (curCellBuildDataComp.IsBuildType(BuildingTypes.Woodcutter))
                        {
                            ++amountWoodcutter;
                        }

                        else if (curCellBuildDataComp.IsBuildType(BuildingTypes.Mine))
                        {
                            ++amountMine;
                        }
                    }
                }
            }
        }

        var amountUpgradesFarm = amountBuildUpgradesCom.GetAmountUpgrades(BuildingTypes.Farm, PhotonNetwork.IsMasterClient);
        var extractionOneFarm = ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Farm, amountUpgradesFarm);

        var amountAddingFood = 1 + amountFarm * extractionOneFarm - amountUnitsInGame;


        if (amountAddingFood < 0) economyViewUICom.SetAddingText(ResourceTypes.Food, amountAddingFood.ToString());

        else economyViewUICom.SetAddingText(ResourceTypes.Food, "+ " + amountAddingFood.ToString());



        var amountUpgradesWoodcutter = amountBuildUpgradesCom.GetAmountUpgrades(BuildingTypes.Woodcutter, PhotonNetwork.IsMasterClient);
        amountAddingWood += (byte)(amountWoodcutter * ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Woodcutter, amountUpgradesWoodcutter));

        economyViewUICom.SetAddingText(ResourceTypes.Wood, "+ " + amountAddingWood);



        var amountUpgradesMine = amountBuildUpgradesCom.GetAmountUpgrades(BuildingTypes.Mine, PhotonNetwork.IsMasterClient);
        var amountAddingOre = amountMine * ExtractionInfoSupport.GetExtractionOneBuilding(BuildingTypes.Mine, amountUpgradesMine);

        economyViewUICom.SetAddingText(ResourceTypes.Ore, "+ " + amountAddingOre);

        economyViewUICom.SetMainText(ResourceTypes.Food, amountResCom.AmountResources(ResourceTypes.Food, PhotonNetwork.IsMasterClient).ToString());
        economyViewUICom.SetMainText(ResourceTypes.Wood, amountResCom.AmountResources(ResourceTypes.Wood, PhotonNetwork.IsMasterClient).ToString());
        economyViewUICom.SetMainText(ResourceTypes.Ore, amountResCom.AmountResources(ResourceTypes.Ore, PhotonNetwork.IsMasterClient).ToString());
        economyViewUICom.SetMainText(ResourceTypes.Iron, amountResCom.AmountResources(ResourceTypes.Iron, PhotonNetwork.IsMasterClient).ToString());
        economyViewUICom.SetMainText(ResourceTypes.Gold, amountResCom.AmountResources(ResourceTypes.Gold, PhotonNetwork.IsMasterClient).ToString());
    }
}
