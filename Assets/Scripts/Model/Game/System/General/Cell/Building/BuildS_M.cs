using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class BuildS_M : SystemModel
    {
        internal BuildS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void Build(in BuildingTypes buildingT, in LevelTypes levelT, in PlayerTypes playerT, in float hp, in byte cell_0)
        {
            eMG.BuildingTC(cell_0).BuildingT = buildingT;
            eMG.BuildingLevelTC(cell_0).LevelT = levelT;
            eMG.BuildingPlayerTC(cell_0).PlayerT = playerT;
            eMG.BuildingHpC(cell_0).Health = hp;

            if(buildingT == BuildingTypes.Farm)
            {
                eMG.PlayerInfoE(playerT).AmountFarmsInGame++;
            }
        }
    }
}