using System.Collections.Generic;

namespace Chessy.Game
{
    public sealed class ForPlayerPoolEs
    {
        readonly Dictionary<BuildingTypes, bool> _haveBuilding;

        public bool IsReadyC;

        public float PeopleInCity;
        public int MaxAvailablePawns;
        public float WoodForBuyHouse;

        public UnitTC AvailableHeroTC;
        public CooldownC HeroCooldownC;
        public bool HaveHeroInInventor;

        public bool HaveKingInInventor;


        readonly PlayerLevelInfoE[] _levelInfoEs;
        readonly ResourcesC[] _resourceCs;
        readonly Dictionary<UnitTypes, PlayerUnitInfoE> _unitEs;
        public ref PlayerLevelInfoE LevelE(in LevelTypes levT) => ref _levelInfoEs[(byte)levT - 1];
        public ref ResourcesC ResourcesC(in ResourceTypes resT) => ref _resourceCs[(byte)resT - 1];
        public PlayerUnitInfoE UnitE(in UnitTypes unitT) => _unitEs[unitT];

        public bool HaveBuilding(in BuildingTypes buildingT) => _haveBuilding[buildingT];

        internal ForPlayerPoolEs(in bool b)
        {
            PeopleInCity = StartValues.PEOPLE_IN_CITY;
            MaxAvailablePawns = StartValues.MAX_AVAILABLE_PAWN;

            _levelInfoEs = new PlayerLevelInfoE[(byte)LevelTypes.End - 1];
            _resourceCs = new ResourcesC[(byte)ResourceTypes.End - 1];
            _unitEs = new Dictionary<UnitTypes, PlayerUnitInfoE>();


            HaveKingInInventor = true;

            _haveBuilding = new Dictionary<BuildingTypes, bool>();

            _haveBuilding.Add(BuildingTypes.Market, false);
            _haveBuilding.Add(BuildingTypes.Smelter, false);


            WoodForBuyHouse = StartValues.NEED_WOOD_FOR_BUILDING_HOUSE;

            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
            {
                _resourceCs[(byte)resT - 1] = new ResourcesC(StartValues.Resources(resT));
            }
            for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
            {
                _levelInfoEs[(byte)levT - 1] = new PlayerLevelInfoE(levT);
            }
            for (var unitT = UnitTypes.None + 1; unitT < UnitTypes.End; unitT++)
            {
                _unitEs.Add(unitT, new PlayerUnitInfoE(unitT));
            }
        }

        public void SetHaveBuilding(in BuildingTypes buildingT, in bool have) => _haveBuilding[buildingT] = have;
    }
}