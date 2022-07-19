namespace Chessy.Model
{
    public struct WhereUnitCanAttackToEnemyC
    {
        public bool[] WhereUnitCanAttack;

        public ref bool Can(in byte cellIdx) => ref WhereUnitCanAttack[cellIdx];

        internal WhereUnitCanAttackToEnemyC(in bool[] where)
        {
            WhereUnitCanAttack = where;
        }

        internal void Set(in byte idxCell, in bool canAttack) => WhereUnitCanAttack[idxCell] = canAttack;
    }
}