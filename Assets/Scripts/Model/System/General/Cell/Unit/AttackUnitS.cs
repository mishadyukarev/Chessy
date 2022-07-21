using Chessy.Model.Values;
using System;

namespace Chessy.Model.System
{
    public partial class SystemsModel
    {
        internal void AttackUnitOnCell(in double damage, in PlayerTypes whoKiller, in byte cellIdx)
        {
            if (!_unitCs[cellIdx].HaveUnit) throw new Exception();
            if (damage <= 0) throw new Exception();
            if (_cellCs[cellIdx].IsBorder) throw new Exception();

            _hpUnitCs[cellIdx].Health -= damage;
            if (_hpUnitCs[cellIdx].Health <= HpUnitValues.HP_FOR_DEATH_AFTER_ATTACK)
                KillUnit(whoKiller, cellIdx);
        }
    }
}