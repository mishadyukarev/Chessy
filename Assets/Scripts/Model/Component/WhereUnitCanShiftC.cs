namespace Chessy.Model
{
    public sealed class WhereUnitCanShiftC
    {
        internal readonly bool[] WhereArray;

        public bool CanShiftHere(in byte cellIdx) => WhereArray[cellIdx];

        internal WhereUnitCanShiftC(in bool[] cells) => WhereArray = cells;

        internal void Set(in byte cellIdx, in bool canShift) => WhereArray[cellIdx] = canShift;
    }
}