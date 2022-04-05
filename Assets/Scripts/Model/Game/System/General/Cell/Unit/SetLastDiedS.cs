using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class SetLastDiedS : SystemModelGameAbs
    {
        internal SetLastDiedS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Set(in UnitTypes unitT, in LevelTypes levelT, in PlayerTypes playerT, in byte cell_0)
        {
            eMG.LastDiedUnitTC(cell_0).UnitT = unitT;
            eMG.LastDiedLevelTC(cell_0).LevelT = levelT;
            eMG.LastDiedPlayerTC(cell_0).PlayerT = playerT;
        }

        internal void Set(in byte cell_from, in byte cell_to)
        {
            eMG.LastDiedUnitTC(cell_to) = eMG.LastDiedUnitTC(cell_from);
            eMG.LastDiedPlayerTC(cell_to) = eMG.LastDiedPlayerTC(cell_from);
            eMG.LastDiedLevelTC(cell_to) = eMG.LastDiedLevelTC(cell_from);
        }
        internal void Set(in byte cell_0)
        {
            eMG.LastDiedUnitTC(cell_0) = eMG.UnitTC(cell_0);
            eMG.LastDiedPlayerTC(cell_0) = eMG.UnitPlayerTC(cell_0);
            eMG.LastDiedLevelTC(cell_0) = eMG.UnitLevelTC(cell_0);
        }
    }
}