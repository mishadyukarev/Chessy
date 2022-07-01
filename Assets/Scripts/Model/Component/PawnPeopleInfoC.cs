namespace Chessy.Model.Component
{
    public struct PawnPeopleInfoC
    {
        public int AmountInGame;
        public int PeopleInCity;

        public bool HaveAnyPeopleInCity => PeopleInCity >= 1;
        public bool CanGetPawn(int builtHousesAmountInTown) => AmountInGame < MaxAvailablePawns(builtHousesAmountInTown) && HaveAnyPeopleInCity;
        public int MaxAvailablePawns(int builtHousesAmountInTown) => ++builtHousesAmountInTown;
    }
}