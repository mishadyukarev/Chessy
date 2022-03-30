using Chessy.Game.Entity.Model;
using Chessy.Game.Model.System;
using Chessy.Game.Values.Cell.Unit.Stats;
using System;

namespace Chessy.Game.System.Model
{
    sealed class AttackUnitS : SystemModelGameAbs
    {
        internal AttackUnitS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Attack(in float damage, in PlayerTypes whoKiller, in byte cell_0)
        {
            if (damage <= 0) throw new Exception();
            if (!e.CellEs(cell_0).IsActiveParentSelf) throw new Exception();


            ref var health = ref e.UnitHpC(cell_0).Health;

            health -= damage;
            if (health <= HpValues.HP_FOR_DEATH_AFTER_ATTACK)
                s.KillUnitS.Kill(whoKiller, cell_0);
        }
    }
}