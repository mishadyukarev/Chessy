using System;

namespace Scripts.Game
{
    public struct CellBuildDataCom
    {
        public BuildingTypes BuildType;

        public bool HaveBuild => BuildType != BuildingTypes.None;
        public bool Is(BuildingTypes buildingType) => BuildType == buildingType;
        public void DefBuildType() => BuildType = default;   
    }
}