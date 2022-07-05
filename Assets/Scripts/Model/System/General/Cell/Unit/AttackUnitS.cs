using Chessy.Model.Values;
using System;

namespace Chessy.Model.System
{
    public partial class SystemsModel
    {
        internal void AttackUnitOnCell(in double damage, in PlayerTypes whoKiller, in byte cellIdx)
        {
            if (!_e.UnitT(cellIdx).HaveUnit()) throw new Exception();
            if (damage <= 0) throw new Exception();
            if (_e.IsBorder(cellIdx)) throw new Exception();

            _e.HpUnitC(cellIdx).Health -= damage;
            if (_e.HpUnitC(cellIdx).Health <= HpValues.HP_FOR_DEATH_AFTER_ATTACK)
                KillUnit(whoKiller, cellIdx);
        }
    }
}