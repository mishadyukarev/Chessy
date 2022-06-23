using Chessy.Model.Values.Cell.Unit.Stats;
using System;

namespace Chessy.Model.Model.System
{
    sealed partial class UnitSystems
    {
        internal void Attack(in double damage, in PlayerTypes whoKiller, in byte cellIdx)
        {
            //if (!eMG.UnitTC(cellIdx).HaveUnit()) throw new Exception();
            if (damage <= 0) throw new Exception();
            if (_e.IsBorder(cellIdx)) throw new Exception();

            _e.HpUnitC(cellIdx).Health -= damage;
            if (_e.HpUnitC(cellIdx).Health <= HpValues.HP_FOR_DEATH_AFTER_ATTACK)
                KillUnit(whoKiller, cellIdx);
        }
    }
}