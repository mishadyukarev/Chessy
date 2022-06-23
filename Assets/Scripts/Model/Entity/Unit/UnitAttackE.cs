using System.Collections.Generic;

namespace Chessy.Model.Model.Entity
{
    public struct UnitAttackE
    {
        public readonly IdxsCellsC Simple;
        public readonly IdxsCellsC Unique;

        internal UnitAttackE(in HashSet<byte> simpleAttack, in HashSet<byte> uniqueAttack)
        {
            Simple = new IdxsCellsC(simpleAttack);
            Unique = new IdxsCellsC(uniqueAttack);
        }
    }
}