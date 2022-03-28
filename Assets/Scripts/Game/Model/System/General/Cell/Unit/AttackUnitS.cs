using Chessy.Game.Model.System;
using Chessy.Game.Values.Cell.Unit.Stats;
using System;

namespace Chessy.Game.System.Model
{
    sealed class AttackUnitS
    {
        readonly CellEs _cellEs;
        readonly KillUnitS _killUnitS;

        internal AttackUnitS(in CellEs cellEs, in KillUnitS killUnitS)
        {
            _cellEs = cellEs;
            _killUnitS = killUnitS;
        }

        internal void Attack(in float damage, in PlayerTypes whoKiller)
        {
            if (damage <= 0) throw new Exception();
            if (!_cellEs.IsActiveParentSelf) throw new Exception();


            ref var health = ref _cellEs.UnitStatsE.HealthC.Health;

            health -= damage;
            if (health <= HpValues.HP_FOR_DEATH_AFTER_ATTACK)
                _killUnitS.Kill(whoKiller);
        }
    }
}