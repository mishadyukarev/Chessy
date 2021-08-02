internal struct BuildingTypeComponent
{
    internal BuildingTypes BuildingType { get; set; }

    internal bool HaveBuild => BuildingType != BuildingTypes.None;
    internal bool Is(BuildingTypes buildingType) => BuildingType == buildingType;
    internal void Reset() => BuildingType = default;
}
