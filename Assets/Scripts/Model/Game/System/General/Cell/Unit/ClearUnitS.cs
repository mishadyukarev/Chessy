using Chessy.Game.Model.Entity.Cell.Unit;

namespace Chessy.Game.Model.System
{
    static class ClearUnitS
    {
        internal static void ClearEverything(this UnitEs unitEs)
        {
            unitEs.MainE.UnitT = UnitTypes.None;
        }
    }
}