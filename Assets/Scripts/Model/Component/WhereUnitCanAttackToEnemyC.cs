namespace Chessy.Model
{
    public struct WhereUnitCanAttackToEnemyC
    {
        readonly bool[] _where;

        public bool Can(in byte cellIdx) => _where[cellIdx];

        internal WhereUnitCanAttackToEnemyC(in bool[] where)
        {
            _where = where;
        }

        internal void Set(in byte idxCell, in bool canAttack) => _where[idxCell] = canAttack;
    }
}