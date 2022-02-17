namespace Game.Game
{
    public struct InfoForPlayerE
    {
        public UnitTC AvailableHeroTC;
        public bool IsReadyC;
        public bool HaveCenterHeroC;

        public int PeopleInCityC;
        public int MaxAvailablePawnsC;

        readonly UnitInfoE[] _unitsInfoEs;
        public ref UnitInfoE UnitsInfoE(in UnitTypes unitT) => ref _unitsInfoEs[(byte)unitT];

        internal InfoForPlayerE(in bool b) : this()
        {
            PeopleInCityC = StartValues.PEOPLE_IN_CITY;
            MaxAvailablePawnsC = StartValues.MAX_AVAILABLE_PAWN;

            _unitsInfoEs = new UnitInfoE[(byte)UnitTypes.End];
        }
    }
}