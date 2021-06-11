using System;

internal struct BuildingTypeComponent : IDisposable
{
    internal BuildingTypes BuildingType;

    internal bool HaveBuilding => BuildingType != BuildingTypes.None;

    public void Dispose()
    {
        BuildingType = default;
    }
}
