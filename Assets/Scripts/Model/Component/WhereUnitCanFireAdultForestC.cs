namespace Chessy.Model
{
    public struct WhereUnitCanFireAdultForestC
    {
        readonly bool[] _where;

        public ref bool Can(in byte cellIdx) => ref _where[cellIdx];
        public bool[] Where => (bool[])_where.Clone();

        internal WhereUnitCanFireAdultForestC(in bool[] where)
        {
            _where = where;
        }

        internal void Set(in byte idxCell, in bool canAttack) => _where[idxCell] = canAttack;
    }
}