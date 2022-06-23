using Chessy.Model.Model.Entity.Cell.Unit;

namespace Chessy.Model.Model.System
{
    static class ClearUnitS
    {
        internal static void ClearEverything(this UnitEs unitEs)
        {
            unitEs.MainE.UnitT = UnitTypes.None;
        }
    }
}