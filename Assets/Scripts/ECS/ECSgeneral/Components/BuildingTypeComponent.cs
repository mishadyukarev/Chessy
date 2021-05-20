internal struct BuildingTypeComponent
{
    internal BuildingTypes BuildingType;

    internal bool HaveBuilding => BuildingType != BuildingTypes.None;
}
