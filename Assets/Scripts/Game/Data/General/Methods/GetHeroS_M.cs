namespace Chessy.Game.System.Model.Master
{
    public struct GetHeroS_M
    {
        public GetHeroS_M(in UnitTypes unitT, in EntitiesModel e)
        {
            var whoseMove = e.WhoseMove.Player;

            e.PlayerInfoE(whoseMove).AvailableHeroTC.Unit = unitT;
            e.PlayerInfoE(whoseMove).HaveHeroInInventor = true;
        }
    }
}