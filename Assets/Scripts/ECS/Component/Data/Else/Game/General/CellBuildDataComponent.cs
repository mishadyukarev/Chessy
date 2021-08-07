internal struct CellBuildDataComponent
{
    internal BuildingTypes BuildingType { get; set; }
    internal int TimeSteps { get; set; }


    internal bool HaveBuild => BuildingType != BuildingTypes.None;
    internal bool IsBuildType(BuildingTypes buildingType) => BuildingType == buildingType;
    internal void ResetBuilType() => BuildingType = default;

    internal void AddTimeSteps(int add = 1) => TimeSteps += add;
    internal void TakeTimeSteps(int take = 1) => TimeSteps -= take;
    internal void ResetTimeSteps() => TimeSteps = default;
}
