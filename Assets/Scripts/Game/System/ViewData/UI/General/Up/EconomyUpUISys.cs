using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class EconomyUpUISys : IEcsRunSystem
    {
        private readonly EcsFilter<EnvC> _envF = default;
        private readonly EcsFilter<ConditionUnitC> _unitF = default;

        public void Run()
        {
            var curPlayer = WhoseMoveC.CurPlayerI;

            var amountUnitsInGame = WhereUnitsC.AmountUnitsExcept(curPlayer, UnitTypes.King);
            var percUpgFarms = BuildsUpgC.PercUpg(BuildTypes.Farm, curPlayer);
            var amountFarms = WhereBuildsC.AmountBuilds(curPlayer, BuildTypes.Farm);
            var amountAddFood = Extractor.GetAddFood(percUpgFarms, amountFarms, amountUnitsInGame);

            if (amountAddFood < 0) EconomyUIC.SetAddText(ResTypes.Food, amountAddFood.ToString());
            else EconomyUIC.SetAddText(ResTypes.Food, "+ " + amountAddFood.ToString());



            var amountWoodcutter = WhereBuildsC.AmountBuilds(curPlayer, BuildTypes.Woodcutter);
            var extOneWoodcut = Extractor.GetExtractOneBuild(BuildsUpgC.PercUpg(BuildTypes.Woodcutter, curPlayer));
            var amountAddWood = 0;
            foreach (var idx_0 in WhereUnitsC.IdxsUnits(curPlayer, UnitTypes.Pawn, LevelTypes.First))
            {
                if (_envF.Get1(idx_0).Have(EnvTypes.AdultForest))
                    if (_unitF.Get1(idx_0).Is(CondUnitTypes.Relaxed))
                        amountAddWood += 1;
            }
            foreach (var idx_0 in WhereUnitsC.IdxsUnits(curPlayer, UnitTypes.Pawn, LevelTypes.Second))
            {
                if (_envF.Get1(idx_0).Have(EnvTypes.AdultForest))
                    if (_unitF.Get1(idx_0).Is(CondUnitTypes.Relaxed))
                        amountAddWood += 2;
            }
            amountAddWood += amountWoodcutter * extOneWoodcut;
            EconomyUIC.SetAddText(ResTypes.Wood, "+ " + amountAddWood);


            var amountAddOre = WhereBuildsC.AmountBuilds(curPlayer, BuildTypes.Mine)
                * Extractor.GetExtractOneBuild(BuildsUpgC.PercUpg(BuildTypes.Mine, curPlayer));
            EconomyUIC.SetAddText(ResTypes.Ore, "+ " + amountAddOre);




            EconomyUIC.SetMainText(ResTypes.Food, InvResC.AmountRes(curPlayer, ResTypes.Food).ToString());
            EconomyUIC.SetMainText(ResTypes.Wood, InvResC.AmountRes(curPlayer, ResTypes.Wood).ToString());
            EconomyUIC.SetMainText(ResTypes.Ore, InvResC.AmountRes(curPlayer, ResTypes.Ore).ToString());
            EconomyUIC.SetMainText(ResTypes.Iron, InvResC.AmountRes(curPlayer, ResTypes.Iron).ToString());
            EconomyUIC.SetMainText(ResTypes.Gold, InvResC.AmountRes(curPlayer, ResTypes.Gold).ToString());

        }
    }
}