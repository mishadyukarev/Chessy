using Chessy.Model.Model.Entity.Cell.Unit;

namespace Chessy.Model.Model.System
{
    static class ClearUnitS
    {
        internal static void ClearEverything(this UnitE unitEs)
        {
            unitEs.MainC.UnitT = UnitTypes.None;
        }
    }
}