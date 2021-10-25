using System;

namespace Scripts.Game
{
    internal struct CellBuildDataCom
    {
        internal BuildingTypes BuildType;

        internal bool HaveBuild => BuildType != BuildingTypes.None;
        internal bool Is(BuildingTypes buildingType) => BuildType == buildingType;
        internal void DefBuildType() => BuildType = default;   
    }
}