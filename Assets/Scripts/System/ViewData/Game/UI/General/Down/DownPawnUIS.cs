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

            var amountPawnsInGame = Es.ForPlayerE(curPlayerI).UnitsInfoE(UnitTypes.Pawn).UnitsInGameC;

            DownPawnUIE.TextUIC.Text = amountPawnsInGame.ToString() + "/" + Es.ForPlayerE(curPlayerI).MaxAvailablePawnsC;
            UIEs.DownEs.PawnEs.MaxPeopleE.SetMaxPeople(Es.ForPlayerE(curPlayerI).PeopleInCityC);
        }
    }
}