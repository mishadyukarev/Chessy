internal struct CellBuildDataComponent
{
    internal BuildingTypes BuildingType { get; set; }

    internal bool HaveBuild => BuildingType != BuildingTypes.None;
    internal bool IsBuildType(BuildingTypes buildingType) => BuildingType == buildingType;
    internal void ResetBuildType() => BuildingType = default;
}
