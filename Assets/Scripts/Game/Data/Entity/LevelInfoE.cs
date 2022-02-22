using System.Collections.Generic;

namespace Game.Game
{
    public struct LevelInfoE
    {
        readonly AmountC[] _twTC;
        readonly UnitInfoE[] _unitsInfoEs;
        readonly InfoPlayerLevelBuildingE[] _buildingInfoEs;

        public ref AmountC ToolWeapons(in ToolWeaponTypes tw) => ref _twTC[(byte)tw];
        public ref UnitInfoE UnitsInfoE(in UnitTypes unitT) => ref _unitsInfoEs[(byte)unitT - 1];
        public ref InfoPlayerLevelBuildingE BuildingInfoE(in BuildingTypes buildT) => ref _buildingInfoEs[(byte)buildT - 1];

        public LevelInfoE(in LevelTypes levT)
        {
            _twTC = new AmountC[(byte)ToolWeaponTypes.End];
            _buildingInfoEs = new InfoPlayerLevelBuildingE[(byte)BuildingTypes.End];

            for (var buildT = BuildingTypes.None + 1; buildT < BuildingTypes.End; buildT++)
            {
                _buildingInfoEs[(byte)buildT - 1] = new InfoPlayerLevelBuildingE(default);
            }

            _unitsInfoEs = new UnitInfoE[(byte)UnitTypes.End - 1];
            for (var unitT = UnitTypes.None + 1; unitT < UnitTypes.End; unitT++)
            {
                _unitsInfoEs[(byte)unitT - 1] = new UnitInfoE(levT, unitT, Start_Values.HaveUnit(unitT, LevelTypes.First));
            }
        }
    }
}