namespace Chessy.Game
{
    sealed class DownPawnUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal DownPawnUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            //var curPlayerI = E.CurPlayerITC.Player;

            //var amountPawnsInGame = E.UnitInfo(curPlayerI, LevelTypes.First, UnitTypes.Pawn).UnitsInGame
            //    + E.UnitInfo(curPlayerI, LevelTypes.Second, UnitTypes.Pawn).UnitsInGame;

            //DownPawnUIE.TextUIC.Text = amountPawnsInGame.ToString() + "/" + E.PlayerE(curPlayerI).MaxAvailablePawns;
            //UIEs.DownEs.PawnEs.MaxPeopleE.SetMaxPeople(E.PlayerE(curPlayerI).PeopleInCity);
        }
    }
}