using Leopotam.Ecs;
using System.Collections.Generic;

namespace Scripts.Game
{
    internal sealed class EconomyUpUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, ConditionUnitC, OwnerCom> _cellUnitsFilter = default;
        private EcsFilter<CellBuildDataCom, OwnerCom> _cellBuildFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvDatFilt = default;

        private EcsFilter<UpgBuildsC> _upgradeBuildsFilter = default;


        public void Run()
        {
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
                ref var condUnitC = ref _cellUnitsFilter.Get2(curIdxCell);
                ref var curOwnUnitCom = ref _cellUnitsFilter.Get3(curIdxCell);

                ref var curBuildDatCom = ref _cellBuildFilter.Get1(curIdxCell);
                ref var curOnBuildCom = ref _cellBuildFilter.Get2(curIdxCell);


                if (curUnitDatCom.HaveUnit)
                {
                    if (curOwnUnitCom.IsMine)
                    {
                        if (!curUnitDatCom.Is(UnitTypes.King)) ++amountUnitsInGame;

                        if (curUnitDatCom.Is(UnitTypes.Pawn))
                        {
                            if (condUnitC.Is(CondUnitTypes.Relaxed))
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
            var extractOneFarm = UpgBuildsC.GetExtractOneBuild(WhoseMoveC.CurPlayer, BuildingTypes.Farm);

            var amountAddFood = 3 + builds[BuildingTypes.Farm] * extractOneFarm - amountUnitsInGame;


            if (amountAddFood < 0) EconomyViewUIC.SetAddText(ResourceTypes.Food, amountAddFood.ToString());

            else EconomyViewUIC.SetAddText(ResourceTypes.Food, "+ " + amountAddFood.ToString());



            amountAddWood += (byte)(builds[BuildingTypes.Woodcutter] * UpgBuildsC.GetExtractOneBuild(WhoseMoveC.CurPlayer, BuildingTypes.Woodcutter));
            EconomyViewUIC.SetAddText(ResourceTypes.Wood, "+ " + amountAddWood);

            var amountAddOre = builds[BuildingTypes.Mine] * UpgBuildsC.GetExtractOneBuild(WhoseMoveC.CurPlayer, BuildingTypes.Mine);
            EconomyViewUIC.SetAddText(ResourceTypes.Ore, "+ " + amountAddOre);




            EconomyViewUIC.SetMainText(ResourceTypes.Food, InventResourcesC.AmountRes(WhoseMoveC.CurPlayer, ResourceTypes.Food).ToString());
            EconomyViewUIC.SetMainText(ResourceTypes.Wood, InventResourcesC.AmountRes(WhoseMoveC.CurPlayer, ResourceTypes.Wood).ToString());
            EconomyViewUIC.SetMainText(ResourceTypes.Ore, InventResourcesC.AmountRes(WhoseMoveC.CurPlayer, ResourceTypes.Ore).ToString());
            EconomyViewUIC.SetMainText(ResourceTypes.Iron, InventResourcesC.AmountRes(WhoseMoveC.CurPlayer, ResourceTypes.Iron).ToString());
            EconomyViewUIC.SetMainText(ResourceTypes.Gold, InventResourcesC.AmountRes(WhoseMoveC.CurPlayer, ResourceTypes.Gold).ToString());

        }
    }
}