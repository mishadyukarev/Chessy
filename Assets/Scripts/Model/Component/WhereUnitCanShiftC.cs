namespace Chessy.Model
{
    public struct WhereUnitCanShiftC
    {
        readonly bool[] _whereUnitCanShift;

        public bool CanShiftHere(in byte cellIdx) => _whereUnitCanShift[cellIdx];
        public bool[] WhereUnitCanShift => (bool[])_whereUnitCanShift.Clone();

        internal WhereUnitCanShiftC(in bool[] cells)
        {
            _whereUnitCanShift = cells;
        }

        internal void SetWhereUnitCanShift(in byte idxCell, in bool canShift) => _whereUnitCanShift[idxCell] = canShift;
    }
}