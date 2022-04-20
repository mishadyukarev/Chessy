namespace Chessy.Game
{
    public struct PlayerLevelInfoE
    {
        readonly int[] _toolWeapons;
        readonly PlayerLevelBuildingInfoE[] _buildingInfoEs;

        public ref int ToolWeapons(in ToolWeaponTypes tw) => ref _toolWeapons[(byte)tw];
        public ref PlayerLevelBuildingInfoE BuildingInfoE(in BuildingTypes buildT) => ref _buildingInfoEs[(byte)buildT];

        public PlayerLevelInfoE(in LevelTypes levT) : this()
        {
            _toolWeapons = new int[(byte)ToolWeaponTypes.End];
            _buildingInfoEs = new PlayerLevelBuildingInfoE[(byte)BuildingTypes.End];

            for (var buildT = BuildingTypes.None + 1; buildT < BuildingTypes.End; buildT++)
            {
                _buildingInfoEs[(byte)buildT] = new PlayerLevelBuildingInfoE(default);
            }
        }


        public void StartGame()
        {
            for (var i = 0; i < _toolWeapons.Length; i++)
            {
                _toolWeapons[i] = 0;
            }

            for (var buildT = BuildingTypes.None + 1; buildT < BuildingTypes.End; buildT++)
            {
                _buildingInfoEs[(byte)buildT].IdxC.Clear();
            }
        }
    }
}