﻿using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    internal sealed class BuildS : SystemModel
    {
        internal BuildS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void Build(in BuildingTypes buildingT, in LevelTypes levelT, in PlayerTypes playerT, in float hp, in byte cell_0)
        {
            eMG.BuildingTC(cell_0).BuildingT = buildingT;
            eMG.BuildingLevelTC(cell_0).LevelT = levelT;
            eMG.BuildingPlayerTC(cell_0).PlayerT = playerT;
            eMG.BuildingHpC(cell_0).Health = hp;

            //eMGame.BuildingsInfo(playerT, levelT, buildingT).IdxC.Add(cell_0);
        }
    }
}