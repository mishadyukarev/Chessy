using Chessy.Model.Cell.Unit;

namespace Chessy.Model
{
    static class ClearUnitS
    {
        internal static void ClearEverything(this UnitE unitEs)
        {
            unitEs.MainC.UnitT = UnitTypes.None;
        }
    }
}