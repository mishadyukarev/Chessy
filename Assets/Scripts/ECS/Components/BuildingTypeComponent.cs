internal struct BuildingTypeComponent
{
    private BuildingTypes _buildingTypes;

    internal BuildingTypes BuildingType => _buildingTypes;
    internal bool HaveBuilding => _buildingTypes != BuildingTypes.None;

    internal void StartFill(BuildingTypes buildingType = default) => _buildingTypes = buildingType;
    internal void SetBuildingType(BuildingTypes buildingType) => _buildingTypes = buildingType;
    internal void ResetBuildingType() => _buildingTypes = default;
}
