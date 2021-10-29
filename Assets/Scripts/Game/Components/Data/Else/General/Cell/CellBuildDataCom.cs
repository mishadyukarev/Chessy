namespace Scripts.Game
{
    public struct CellBuildDataCom
    {
        public BuildingTypes BuildType { get; set; }
        public bool HaveBuild => BuildType != BuildingTypes.None;

        public bool Is(BuildingTypes buildingType) => BuildType == buildingType;
        public void Def() => BuildType = default;
    }
}