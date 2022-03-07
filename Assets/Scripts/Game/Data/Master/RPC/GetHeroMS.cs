namespace Chessy.Game
{
    sealed class GetHeroMS : SystemAbstract, IEcsRunSystem
    {
        internal GetHeroMS(in EntitiesModel ents) : base(ents)
        {

        }

        public void Run()
        {
            var unitT = E.RpcPoolEs.GetHeroTC.Unit;

            if (unitT != UnitTypes.None)
            {
                var whoseMove = E.WhoseMove.Player;

                E.PlayerE(whoseMove).AvailableHeroTC.Unit = unitT;
                E.UnitInfoE(whoseMove, LevelTypes.First, unitT).HaveInInventor = true;
                E.PlayerE(whoseMove).HaveCenterHero = false;


                E.RpcPoolEs.GetHeroTC.Unit = UnitTypes.None;
            }
        }
    }
}