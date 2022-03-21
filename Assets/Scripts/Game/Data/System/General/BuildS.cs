namespace Chessy.Game.System.Model
{
    public struct BuildS
    {
        public BuildS(in BuildingTypes buildingT, in LevelTypes levelT, in PlayerTypes playerT, in float hp, in byte idx_0, in Chessy.Game.Entity.Model.EntitiesModel e)
        {
            e.BuildingTC(idx_0).Building = buildingT;
            e.BuildingLevelTC(idx_0).Level = levelT;
            e.BuildingPlayerTC(idx_0).Player = playerT;
            e.BuildHpC(idx_0).Health = hp;

            e.BuildingsInfo(playerT, levelT, buildingT).IdxC.Add(idx_0);
        }
    }
}