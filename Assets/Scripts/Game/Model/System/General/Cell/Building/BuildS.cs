using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    internal sealed class BuildS : SystemModelGameAbs
    {
        internal BuildS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Build(in BuildingTypes buildingT, in LevelTypes levelT, in PlayerTypes playerT, in float hp, in byte cell_0)
        {
            e.BuildingTC(cell_0).Building = buildingT;
            e.BuildingLevelTC(cell_0).Level = levelT;
            e.BuildingPlayerTC(cell_0).Player = playerT;
            e.BuildingHpC(cell_0).Health = hp;

            //eMGame.BuildingsInfo(playerT, levelT, buildingT).IdxC.Add(cell_0);
        }
    }
}