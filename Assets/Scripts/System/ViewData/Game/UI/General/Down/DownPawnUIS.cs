namespace Game.Game
{
    sealed class DownPawnUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal DownPawnUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var curPlayerI = Es.WhoseMovePlayerTC.CurPlayerI;

            var amountPawns = Es.WhereWorker.AmountPaws(curPlayerI);

            DownPawnUIE.TextUIC.Text = amountPawns.ToString() + "/" + Es.MaxAvailablePawnsE(curPlayerI).MaxPawns;
            UIEs.DownEs.PawnEs.MaxPeopleE.SetMaxPeople(Es.PeopleInCityE(curPlayerI).AmountC.Amount);
        }
    }
}