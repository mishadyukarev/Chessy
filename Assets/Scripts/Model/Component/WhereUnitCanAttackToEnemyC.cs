namespace Chessy.Model
{
    public struct WhereUnitCanAttackToEnemyC
    {
        public bool[] WhereUnitCanAttack;

        public bool Can(in byte cellIdx) => WhereUnitCanAttack[cellIdx];

        internal WhereUnitCanAttackToEnemyC(in bool[] where)
        {
            WhereUnitCanAttack = where;
        }

        internal void Set(in byte idxCell, in bool canAttack) => WhereUnitCanAttack[idxCell] = canAttack;
    }
}