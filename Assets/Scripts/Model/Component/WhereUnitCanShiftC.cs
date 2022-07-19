namespace Chessy.Model
{
    public readonly struct WhereUnitCanShiftC
    {
        readonly bool[] _where;

        public ref bool CanShiftHere(in byte cellIdx) => ref _where[cellIdx];
        public bool[] Where => (bool[])_where.Clone();

        internal WhereUnitCanShiftC(in bool[] cells) => _where = cells;

        internal void Set(in byte cellIdx, in bool canShift) => _where[cellIdx] = canShift;
    }
}