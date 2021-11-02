using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class EconomyUpUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, ConditionUnitC, OwnerCom> _cellUnitsFilter = default;
        private EcsFilter<CellBuildDataC, OwnerCom> _cellBuildFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvDatFilt = default;


        public void Run()
        {
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
                    if (curOwnUnitCom.Is(WhoseMoveC.CurPlayer))
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

            var haveUpgFarms = BuildsUpgC.HaveUpgrade(WhoseMoveC.CurPlayer, BuildTypes.Farm);
            var amountFarms = WhereBuildsC.AmountBuilds(WhoseMoveC.CurPlayer, BuildTypes.Farm);
            var amountAddFood = ExtractC.GetAddFood(haveUpgFarms, amountFarms, amountUnitsInGame);

            if (amountAddFood < 0) EconomyViewUIC.SetAddText(ResTypes.Food, amountAddFood.ToString());
            else EconomyViewUIC.SetAddText(ResTypes.Food, "+ " + amountAddFood.ToString());



            amountAddWood += (byte)(WhereBuildsC.AmountBuilds(WhoseMoveC.CurPlayer, BuildTypes.Woodcutter)
                * ExtractC.GetExtractOneBuild(BuildsUpgC.HaveUpgrade(WhoseMoveC.CurPlayer, BuildTypes.Woodcutter)));
            EconomyViewUIC.SetAddText(ResTypes.Wood, "+ " + amountAddWood);

            var amountAddOre = WhereBuildsC.AmountBuilds(WhoseMoveC.CurPlayer, BuildTypes.Mine)
                * ExtractC.GetExtractOneBuild(BuildsUpgC.HaveUpgrade(WhoseMoveC.CurPlayer, BuildTypes.Mine));
            EconomyViewUIC.SetAddText(ResTypes.Ore, "+ " + amountAddOre);




            EconomyViewUIC.SetMainText(ResTypes.Food, InventResC.AmountRes(WhoseMoveC.CurPlayer, ResTypes.Food).ToString());
            EconomyViewUIC.SetMainText(ResTypes.Wood, InventResC.AmountRes(WhoseMoveC.CurPlayer, ResTypes.Wood).ToString());
            EconomyViewUIC.SetMainText(ResTypes.Ore, InventResC.AmountRes(WhoseMoveC.CurPlayer, ResTypes.Ore).ToString());
            EconomyViewUIC.SetMainText(ResTypes.Iron, InventResC.AmountRes(WhoseMoveC.CurPlayer, ResTypes.Iron).ToString());
            EconomyViewUIC.SetMainText(ResTypes.Gold, InventResC.AmountRes(WhoseMoveC.CurPlayer, ResTypes.Gold).ToString());

        }
    }
}