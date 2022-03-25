using Chessy.Game.Entity.Model;

namespace Chessy.Game.Model.System
{
    sealed class ClearUnitS : SystemModelGameAbs
    {
        internal ClearUnitS(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Clear(in byte cell_0)
        {
            eMGame.UnitTC(cell_0).Unit = UnitTypes.None;
        }
    }
}