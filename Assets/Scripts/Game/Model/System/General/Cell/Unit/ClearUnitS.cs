using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;

namespace Chessy.Game.Model.System
{
    sealed class ClearUnitS : SystemModelGameAbs
    {
        internal ClearUnitS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Clear(in byte cell_0)
        {
            e.UnitTC(cell_0).Unit = UnitTypes.None;
        }
    }
}