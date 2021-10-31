using Leopotam.Ecs;
using System.Collections.Generic;

namespace Scripts.Game
{
    internal sealed class EconomyUpUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, ConditionUnitC, OwnerCom> _cellUnitsFilter = default;
        private EcsFilter<CellBuildDataC, OwnerCom> _cellBuildFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvDatFilt = default;

        private EcsFilter<UpgBuildsC> _upgradeBuildsFilter = default;


        public void Run()
        {
            ref var amountBuildUpgsCom = ref _upgradeBuildsFilter.Get1(0);

            byte amountUnitsInGame = 0;
            byte amountAddWood = 0;

            foreach (var curIdxCell in _cellBuildFilter)
            {
                ref var unitC_0 = ref _cellUnitsFilter.Get1(curIdxCell);
                ref var condUnitC_0 = ref _cellUnitsFilter.Get2(curIdxCell);
                ref var curOwnUnitCom = ref _cellUnitsFilter.Get3(curIdxCell);

                ref var buildC_0 = ref _cellBuildFilter.Get1(curIdxCell);
                ref var ownBuildC_0 = ref _cellBuildFilter.Get2(curIdxCell);

                ref var envC_0 = ref _cellEnvDatFilt.Get1(curIdxCell);

                if (unitC_0.HaveUnit)
                {
                    if (curOwnUnitCom.IsMine)
                    {
                        if (!unitC_0.Is(UnitTypes.King)) ++amountUnitsInGame;

                        if (unitC_0.Is(UnitTypes.Pawn))
                        {
                            if (condUnitC_0.Is(CondUnitTypes.Relaxed))
                            {
                                if (_cellEnvDatFilt.Get1(curIdxCell).Have(EnvirTypes.AdultForest))
                                {
                                    amountAddWood += 1;
                                }
                            }
                        }
                    }
                }
            }

            var amountAddFood = 3 + WhereBuildsC.AmountBuilds(WhoseMoveC.CurPlayer, BuildTypes.Farm) 
                * UpgBuildsC.GetExtractOneBuild(WhoseMoveC.CurPlayer, BuildTypes.Farm) - amountUnitsInGame;


            if (amountAddFood < 0) EconomyViewUIC.SetAddText(ResourceTypes.Food, amountAddFood.ToString());

            else EconomyViewUIC.SetAddText(ResourceTypes.Food, "+ " + amountAddFood.ToString());



            amountAddWood += (byte)(UpgBuildsC.GetExtractOneBuild(WhoseMoveC.CurPlayer, BuildTypes.Woodcutter) 
                * UpgBuildsC.GetExtractOneBuild(WhoseMoveC.CurPlayer, BuildTypes.Woodcutter));
            EconomyViewUIC.SetAddText(ResourceTypes.Wood, "+ " + amountAddWood);

            var amountAddOre = UpgBuildsC.GetExtractOneBuild(WhoseMoveC.CurPlayer, BuildTypes.Mine)
                * UpgBuildsC.GetExtractOneBuild(WhoseMoveC.CurPlayer, BuildTypes.Mine);
            EconomyViewUIC.SetAddText(ResourceTypes.Ore, "+ " + amountAddOre);




            EconomyViewUIC.SetMainText(ResourceTypes.Food, InventResC.AmountRes(WhoseMoveC.CurPlayer, ResourceTypes.Food).ToString());
            EconomyViewUIC.SetMainText(ResourceTypes.Wood, InventResC.AmountRes(WhoseMoveC.CurPlayer, ResourceTypes.Wood).ToString());
            EconomyViewUIC.SetMainText(ResourceTypes.Ore, InventResC.AmountRes(WhoseMoveC.CurPlayer, ResourceTypes.Ore).ToString());
            EconomyViewUIC.SetMainText(ResourceTypes.Iron, InventResC.AmountRes(WhoseMoveC.CurPlayer, ResourceTypes.Iron).ToString());
            EconomyViewUIC.SetMainText(ResourceTypes.Gold, InventResC.AmountRes(WhoseMoveC.CurPlayer, ResourceTypes.Gold).ToString());

        }
    }
}