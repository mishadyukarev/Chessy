using System.Collections.Generic;

namespace Chessy.Game
{
    public struct LevelInfoE
    {
        readonly AmountC[] _twTC;
        readonly PlayerLevelUnitInfoE[] _unitsInfoEs;
        readonly PlayerLevelBuildingInfoE[] _buildingInfoEs;

        public ref AmountC ToolWeapons(in ToolWeaponTypes tw) => ref _twTC[(byte)tw];
        public ref PlayerLevelUnitInfoE UnitsInfoE(in UnitTypes unitT) => ref _unitsInfoEs[(byte)unitT - 1];
        public ref PlayerLevelBuildingInfoE BuildingInfoE(in BuildingTypes buildT) => ref _buildingInfoEs[(byte)buildT - 1];

        public LevelInfoE(in LevelTypes levT)
        {
            _twTC = new AmountC[(byte)ToolWeaponTypes.End];
            _buildingInfoEs = new PlayerLevelBuildingInfoE[(byte)BuildingTypes.End];

            for (var buildT = BuildingTypes.None + 1; buildT < BuildingTypes.End; buildT++)
            {
                _buildingInfoEs[(byte)buildT - 1] = new PlayerLevelBuildingInfoE(default);
            }

            _unitsInfoEs = new PlayerLevelUnitInfoE[(byte)UnitTypes.End - 1];
            for (var unitT = UnitTypes.None + 1; unitT < UnitTypes.End; unitT++)
            {
                _unitsInfoEs[(byte)unitT - 1] = new PlayerLevelUnitInfoE(levT, unitT, Start_VALUES.HaveUnit(unitT, LevelTypes.First));
            }
        }
    }
}