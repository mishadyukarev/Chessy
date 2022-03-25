using Chessy.Game.Entity.Model;
using Chessy.Game.Model.System;
using Chessy.Game.Values.Cell.Unit.Stats;
using System;

namespace Chessy.Game.System.Model
{
    sealed class AttackUnitS : SystemModelGameAbs
    {
        readonly KillUnitS _killUnitS;

        internal AttackUnitS(in KillUnitS killUnitS, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _killUnitS = killUnitS;
        }

        public void Attack(in float damage, in PlayerTypes whoKiller, in byte cell_0)
        {
            if (damage <= 0) throw new Exception();
            if (!eMGame.CellEs(cell_0).IsActiveParentSelf) throw new Exception();


            ref var health = ref eMGame.UnitHpC(cell_0).Health;

            health -= damage;
            if (health <= HpValues.HP_FOR_DEATH_AFTER_ATTACK)
                _killUnitS.Kill(cell_0, whoKiller);
        }
    }
}