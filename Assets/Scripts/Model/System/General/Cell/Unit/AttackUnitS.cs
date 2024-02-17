using Chessy.Model.Values;
using System;

namespace Chessy.Model.System
{
    public partial class SystemsModel
    {
        internal void AttackUnitOnCell(in double damage, in PlayerTypes whoKiller, in byte cellIdx)
        {
            if (!unitCs[cellIdx].HaveUnit) throw new Exception();
            if (damage <= 0) throw new Exception();
            if (CellC(cellIdx).IsBorder) throw new Exception();

            unitHpCs[cellIdx].Health -= damage;
            if (unitHpCs[cellIdx].Health <= HpUnitValues.HP_FOR_DEATH_AFTER_ATTACK)
                KillUnit(whoKiller, cellIdx);
        }
    }
}