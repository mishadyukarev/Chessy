namespace Chessy.Model
{
    public sealed class WhereUnitCanAttackToEnemyC
    {
        internal readonly bool[] WhereUnitCanAttack;

        public bool Can(in byte cellIdx) => WhereUnitCanAttack[cellIdx];

        internal WhereUnitCanAttackToEnemyC(in bool[] where) { WhereUnitCanAttack = where; }

        internal void Set(in byte idxCell, in bool canAttack) => WhereUnitCanAttack[idxCell] = canAttack;
    }
}