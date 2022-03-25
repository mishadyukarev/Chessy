using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    public sealed class BuildS : SystemModelGameAbs
    {
        public BuildS(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Build(in BuildingTypes buildingT, in LevelTypes levelT, in PlayerTypes playerT, in float hp, in byte cell_0)
        {
            eMGame.BuildingTC(cell_0).Building = buildingT;
            eMGame.BuildingLevelTC(cell_0).Level = levelT;
            eMGame.BuildingPlayerTC(cell_0).Player = playerT;
            eMGame.BuildHpC(cell_0).Health = hp;

            eMGame.BuildingsInfo(playerT, levelT, buildingT).IdxC.Add(cell_0);
        }
    }
}