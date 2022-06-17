namespace Chessy.Game
{
    sealed partial class BuildingSystems
    {
        internal void Build(in BuildingTypes buildingT, in LevelTypes levelT, in PlayerTypes playerT, in float hp, in byte cell_0)
        {
            _eMG.BuildingTC(cell_0).BuildingT = buildingT;
            _eMG.BuildingLevelTC(cell_0).LevelT = levelT;
            _eMG.BuildingPlayerTC(cell_0).PlayerT = playerT;
            _eMG.BuildingHpC(cell_0).Health = hp;

            if (buildingT == BuildingTypes.Farm)
            {
                _eMG.PlayerInfoE(playerT).AmountFarmsInGame++;
            }
        }
    }
}