using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    sealed class SetLastDiedS : SystemModelGameAbs
    {
        internal SetLastDiedS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Set(in UnitTypes unitT, in LevelTypes levelT, in PlayerTypes playerT, in byte cell_0)
        {
            e.LastDiedUnitTC(cell_0).Unit = unitT;
            e.LastDiedLevelTC(cell_0).Level = levelT;
            e.LastDiedPlayerTC(cell_0).Player = playerT;
        }

        internal void Set(in byte cell_from, in byte cell_to) => e.LastDiedE(cell_to) = e.LastDiedE(cell_from);

        internal void Set(in byte cell_0)
        {
            e.LastDiedUnitTC(cell_0) = e.UnitTC(cell_0);
            e.LastDiedPlayerTC(cell_0) = e.UnitPlayerTC(cell_0);
            e.LastDiedLevelTC(cell_0) = e.UnitLevelTC(cell_0);
        }
    }
}