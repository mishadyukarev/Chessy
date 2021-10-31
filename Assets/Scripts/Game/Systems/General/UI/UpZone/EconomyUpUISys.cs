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
                                if (_cellEnvDatFilt.Get1(curIdxCell).Have(EnvTypes.AdultForest))
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


            if (amountAddFood < 0) EconomyViewUIC.SetAddText(ResTypes.Food, amountAddFood.ToString());

            else EconomyViewUIC.SetAddText(ResTypes.Food, "+ " + amountAddFood.ToString());



            amountAddWood += (byte)(UpgBuildsC.GetExtractOneBuild(WhoseMoveC.CurPlayer, BuildTypes.Woodcutter) 
                * UpgBuildsC.GetExtractOneBuild(WhoseMoveC.CurPlayer, BuildTypes.Woodcutter));
            EconomyViewUIC.SetAddText(ResTypes.Wood, "+ " + amountAddWood);

            var amountAddOre = UpgBuildsC.GetExtractOneBuild(WhoseMoveC.CurPlayer, BuildTypes.Mine)
                * UpgBuildsC.GetExtractOneBuild(WhoseMoveC.CurPlayer, BuildTypes.Mine);
            EconomyViewUIC.SetAddText(ResTypes.Ore, "+ " + amountAddOre);




            EconomyViewUIC.SetMainText(ResTypes.Food, InventResC.AmountRes(WhoseMoveC.CurPlayer, ResTypes.Food).ToString());
            EconomyViewUIC.SetMainText(ResTypes.Wood, InventResC.AmountRes(WhoseMoveC.CurPlayer, ResTypes.Wood).ToString());
            EconomyViewUIC.SetMainText(ResTypes.Ore, InventResC.AmountRes(WhoseMoveC.CurPlayer, ResTypes.Ore).ToString());
            EconomyViewUIC.SetMainText(ResTypes.Iron, InventResC.AmountRes(WhoseMoveC.CurPlayer, ResTypes.Iron).ToString());
            EconomyViewUIC.SetMainText(ResTypes.Gold, InventResC.AmountRes(WhoseMoveC.CurPlayer, ResTypes.Gold).ToString());

        }
    }
}