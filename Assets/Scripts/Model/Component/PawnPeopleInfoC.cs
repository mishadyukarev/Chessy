namespace Chessy.Model.Model.Component
{
    public struct PawnPeopleInfoC
    {
        public int MaxAvailable;
        public int AmountInGame;
        public int PeopleInCity;

        public bool HaveAnyPeopleInCity => PeopleInCity >= 1;
        public bool CanGetPawn => AmountInGame < MaxAvailable && HaveAnyPeopleInCity;

        internal void SetPawn() => AmountInGame++;
        internal void RemovePawn() => AmountInGame--;
    }
}