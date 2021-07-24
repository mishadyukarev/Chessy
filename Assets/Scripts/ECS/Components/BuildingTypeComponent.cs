internal struct BuildingTypeComponent
{
    internal BuildingTypes BuildingType { get; set; }

    internal void StartFill(BuildingTypes buildingType = default) => BuildingType = buildingType;
}
