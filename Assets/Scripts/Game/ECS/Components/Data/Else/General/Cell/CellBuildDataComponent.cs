using System;

namespace Scripts.Game
{
    internal struct CellBuildDataComponent
    {
        internal BuildingTypes BuildType;

        internal bool HaveBuild => BuildType != BuildingTypes.None;
        internal bool IsBuildType(BuildingTypes buildingType) => BuildType == buildingType;
        internal void DefBuildType() => BuildType = default;   
    }
}