namespace Chessy.Game
{
    sealed partial class BuildingSystems
    {
        internal void Build(in BuildingTypes buildingT, in LevelTypes levelT, in PlayerTypes playerT, in float hp, in byte cell_0)
        {
            _e.SetBuildingOnCellT(cell_0, buildingT);
            _e.SetBuildingLevelT(cell_0, levelT);
            _e.SetBuildingPlayerT(cell_0, playerT);
            _e.BuildingHpC(cell_0).Health = hp;

            if (buildingT == BuildingTypes.Farm)
            {
                _e.PlayerInfoE(playerT).AmountFarmsInGame++;
            }
        }
    }
}