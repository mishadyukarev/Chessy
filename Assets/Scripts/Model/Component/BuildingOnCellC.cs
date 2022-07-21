namespace Chessy.Model
{
    public sealed class BuildingOnCellC
    {
        internal BuildingTypes BuildingT;
        internal PlayerTypes PlayerT;
        internal LevelTypes LevelT;

        public BuildingTypes BuildingType => BuildingT;
        public PlayerTypes PlayerType => PlayerT;
        public bool HaveBuilding => BuildingT.HaveBuilding();

        internal void Dispose()
        {
            BuildingT = default;
            PlayerT = default;
            LevelT = default;
        }
    }
}