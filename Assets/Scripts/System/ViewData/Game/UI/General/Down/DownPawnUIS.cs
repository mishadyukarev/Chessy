namespace Game.Game
{
    sealed class DownPawnUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal DownPawnUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var curPlayerI = Es.CurPlayerI.Player;

            var amountPawnsInGame = Es.PlayerE(curPlayerI).UnitsInfoE(UnitTypes.Pawn).UnitsInGame;

            DownPawnUIE.TextUIC.Text = amountPawnsInGame.ToString() + "/" + Es.PlayerE(curPlayerI).MaxAvailablePawns;
            UIEs.DownEs.PawnEs.MaxPeopleE.SetMaxPeople(Es.PlayerE(curPlayerI).PeopleInCity);
        }
    }
}