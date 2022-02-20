namespace Game.Game
{
    sealed class DownPawnUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal DownPawnUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var curPlayerI = E.CurPlayerI.Player;

            var amountPawnsInGame = E.PlayerE(curPlayerI).UnitsInfoE(UnitTypes.Pawn).UnitsInGame;

            DownPawnUIE.TextUIC.Text = amountPawnsInGame.ToString() + "/" + E.PlayerE(curPlayerI).MaxAvailablePawns;
            UIEs.DownEs.PawnEs.MaxPeopleE.SetMaxPeople(E.PlayerE(curPlayerI).PeopleInCity);
        }
    }
}