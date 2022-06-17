using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit.Stats;
using System;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitSystems
    {
        internal void Attack(in double damage, in PlayerTypes whoKiller, in byte cellIdx)
        {
            //if (!eMG.UnitTC(cellIdx).HaveUnit) throw new Exception();
            if (damage <= 0) throw new Exception();
            if (_eMG.IsBorder(cellIdx)) throw new Exception();

            _eMG.HpUnitC(cellIdx).Health -= damage;
            if (_eMG.HpUnitC(cellIdx).Health <= HpValues.HP_FOR_DEATH_AFTER_ATTACK)
                KillUnit(whoKiller, cellIdx);
        }
    }
}