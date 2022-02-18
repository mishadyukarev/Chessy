namespace Game.Game
{
    public struct InfoForPlayerE
    {
        public UnitTC AvailableHeroTC;
        public bool IsReadyC;
        public bool HaveCenterHeroC;

        public int PeopleInCity;
        public int MaxAvailablePawns;

        readonly UnitInfoE[] _unitsInfoEs;
        readonly LevelInfoE[] _levelInfoEs;
        readonly ResourcesC[] _resourceCs;
        public ref UnitInfoE UnitsInfoE(in UnitTypes unitT) => ref _unitsInfoEs[(byte)unitT - 1];
        public ref LevelInfoE LevelE(in LevelTypes levT) => ref _levelInfoEs[(byte)levT - 1];
        public ref ResourcesC ResourcesC(in ResourceTypes resT) => ref _resourceCs[(byte)resT - 1];


        internal InfoForPlayerE(in bool b) : this()
        {
            PeopleInCity = StartValues.PEOPLE_IN_CITY;
            MaxAvailablePawns = StartValues.MAX_AVAILABLE_PAWN;

            _unitsInfoEs = new UnitInfoE[(byte)UnitTypes.End - 1];
            _levelInfoEs = new LevelInfoE[(byte)LevelTypes.End - 1];
            _resourceCs = new ResourcesC[(byte)ResourceTypes.End - 1];

            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
            {
                _resourceCs[(byte)resT - 1] = new ResourcesC(StartValues.Resources(resT));
            }
            for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
            {
                _levelInfoEs[(byte)levT - 1] = new LevelInfoE(true);
            }
        }
    }
}