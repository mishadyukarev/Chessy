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
            var haveUpgFarms = BuildsUpgC.HaveUpgrade(curPlayer, BuildTypes.Farm);
            var amountFarms = WhereBuildsC.AmountBuilds(curPlayer, BuildTypes.Farm);
            var amountAddFood = ExtractC.GetAddFood(haveUpgFarms, amountFarms, amountUnitsInGame);

            if (amountAddFood < 0) EconomyViewUIC.SetAddText(ResTypes.Food, amountAddFood.ToString());
            else EconomyViewUIC.SetAddText(ResTypes.Food, "+ " + amountAddFood.ToString());



            var amountWoodcutter = WhereBuildsC.AmountBuilds(curPlayer, BuildTypes.Woodcutter);
            var extOneWoodcut = ExtractC.GetExtractOneBuild(BuildsUpgC.HaveUpgrade(curPlayer, BuildTypes.Woodcutter));
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
            EconomyViewUIC.SetAddText(ResTypes.Wood, "+ " + amountAddWood);


            var amountAddOre = WhereBuildsC.AmountBuilds(curPlayer, BuildTypes.Mine)
                * ExtractC.GetExtractOneBuild(BuildsUpgC.HaveUpgrade(curPlayer, BuildTypes.Mine));
            EconomyViewUIC.SetAddText(ResTypes.Ore, "+ " + amountAddOre);




            EconomyViewUIC.SetMainText(ResTypes.Food, InvResC.AmountRes(curPlayer, ResTypes.Food).ToString());
            EconomyViewUIC.SetMainText(ResTypes.Wood, InvResC.AmountRes(curPlayer, ResTypes.Wood).ToString());
            EconomyViewUIC.SetMainText(ResTypes.Ore, InvResC.AmountRes(curPlayer, ResTypes.Ore).ToString());
            EconomyViewUIC.SetMainText(ResTypes.Iron, InvResC.AmountRes(curPlayer, ResTypes.Iron).ToString());
            EconomyViewUIC.SetMainText(ResTypes.Gold, InvResC.AmountRes(curPlayer, ResTypes.Gold).ToString());

        }
    }
}