using System.Collections.Generic;

namespace Chessy.Game
{
    public struct ForPlayerPoolEs
    {
        public UnitTC AvailableHeroTC;
        public bool IsReadyC;
        public bool HaveCenterHero;
        public bool HaveFraction;

        public float PeopleInCity;
        public float MaxAvailablePawns;
        public bool HaveMarket;
        public bool HaveSmelter;

        readonly LevelInfoE[] _levelInfoEs;
        readonly ResourcesC[] _resourceCs;
        readonly Dictionary<UnitTypes, PlayerUnitInfoE> _unitEs;
        public ref LevelInfoE LevelE(in LevelTypes levT) => ref _levelInfoEs[(byte)levT - 1];
        public ref ResourcesC ResourcesC(in ResourceTypes resT) => ref _resourceCs[(byte)resT - 1];
        public PlayerUnitInfoE UnitE(in UnitTypes unitT) => _unitEs[unitT];

        internal ForPlayerPoolEs(in bool b) : this()
        {
            PeopleInCity = Start_Values.PEOPLE_IN_CITY;
            MaxAvailablePawns = Start_Values.MAX_AVAILABLE_PAWN;

            _levelInfoEs = new LevelInfoE[(byte)LevelTypes.End - 1];
            _resourceCs = new ResourcesC[(byte)ResourceTypes.End - 1];
            _unitEs = new Dictionary<UnitTypes, PlayerUnitInfoE>();

            HaveCenterHero = true;

            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
            {
                _resourceCs[(byte)resT - 1] = new ResourcesC(Start_Values.Resources(resT));
            }
            for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
            {
                _levelInfoEs[(byte)levT - 1] = new LevelInfoE(levT);
            }
            for (var unitT = UnitTypes.None + 1; unitT < UnitTypes.End; unitT++)
            {
                _unitEs.Add(unitT, new PlayerUnitInfoE(unitT));
            }
        }
    }
}