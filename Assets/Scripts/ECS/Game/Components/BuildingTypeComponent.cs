internal struct BuildingTypeComponent
{
    private BuildingTypes _buildingTypes;

    internal BuildingTypes BuildingType
    {
        get => _buildingTypes;
        set => _buildingTypes = value;
    }

    internal bool HaveBuilding => _buildingTypes != BuildingTypes.None;

    internal void StartFill()
    {
        _buildingTypes = BuildingTypes.None;
    }
}
