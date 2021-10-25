﻿using Leopotam.Ecs;
using System.Collections.Generic;

namespace Scripts.Game
{
    internal sealed class EconomyUpUISys : IEcsRunSystem
    {
        private EcsFilter<EconomyViewUICom> _economyUIFilter = default;

        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitsFilter = default;
        private EcsFilter<CellBuildDataCom, OwnerCom> _cellBuildFilter = default;
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
                ref var curOwnUnitCom = ref _cellUnitsFilter.Get2(curIdxCell);

                ref var curBuildDatCom = ref _cellBuildFilter.Get1(curIdxCell);
                ref var curOnBuildCom = ref _cellBuildFilter.Get2(curIdxCell);


                if (curUnitDatCom.HaveUnit)
                {
                    if (curOwnUnitCom.IsMine)
                    {
                        if (!curUnitDatCom.Is(UnitTypes.King)) ++amountUnitsInGame;

                        if (curUnitDatCom.Is(UnitTypes.Pawn))
                        {
                            if (curUnitDatCom.Is(CondUnitTypes.Relaxed))
                            {
                                if (_cellEnvDatFilt.Get1(curIdxCell).Have(EnvirTypes.AdultForest))
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
                        if (curBuildDatCom.Is(BuildingTypes.Farm))
                        {
                            ++builds[BuildingTypes.Farm];
                        }
                        else if (curBuildDatCom.Is(BuildingTypes.Woodcutter))
                        {
                            ++builds[BuildingTypes.Woodcutter];
                        }

                        else if (curBuildDatCom.Is(BuildingTypes.Mine))
                        {
                            ++builds[BuildingTypes.Mine];
                        }

                    }
                }
            }
            var extractOneFarm = amountBuildUpgsCom.GetExtractOneBuild(WhoseMoveCom.CurPlayer, BuildingTypes.Farm);

            var amountAddFood = 3 + builds[BuildingTypes.Farm] * extractOneFarm - amountUnitsInGame;


            if (amountAddFood < 0) econViewUICom.SetAddText(ResourceTypes.Food, amountAddFood.ToString());

            else econViewUICom.SetAddText(ResourceTypes.Food, "+ " + amountAddFood.ToString());



            amountAddWood += (byte)(builds[BuildingTypes.Woodcutter] * amountBuildUpgsCom.GetExtractOneBuild(WhoseMoveCom.CurPlayer, BuildingTypes.Woodcutter));
            econViewUICom.SetAddText(ResourceTypes.Wood, "+ " + amountAddWood);

            var amountAddOre = builds[BuildingTypes.Mine] * amountBuildUpgsCom.GetExtractOneBuild(WhoseMoveCom.CurPlayer, BuildingTypes.Mine);
            econViewUICom.SetAddText(ResourceTypes.Ore, "+ " + amountAddOre);




            econViewUICom.SetMainText(ResourceTypes.Food, amountResCom.AmountRes(WhoseMoveCom.CurPlayer, ResourceTypes.Food).ToString());
            econViewUICom.SetMainText(ResourceTypes.Wood, amountResCom.AmountRes(WhoseMoveCom.CurPlayer, ResourceTypes.Wood).ToString());
            econViewUICom.SetMainText(ResourceTypes.Ore, amountResCom.AmountRes(WhoseMoveCom.CurPlayer, ResourceTypes.Ore).ToString());
            econViewUICom.SetMainText(ResourceTypes.Iron, amountResCom.AmountRes(WhoseMoveCom.CurPlayer, ResourceTypes.Iron).ToString());
            econViewUICom.SetMainText(ResourceTypes.Gold, amountResCom.AmountRes(WhoseMoveCom.CurPlayer, ResourceTypes.Gold).ToString());

        }
    }
}