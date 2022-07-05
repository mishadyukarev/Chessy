namespace Chessy.Model
{
    static class ClearUnitS
    {
        internal static void ClearEverything(this ref UnitE unitEs)
        {
            unitEs.MainC.UnitT = UnitTypes.None;
        }
    }
}