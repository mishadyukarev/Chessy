using Chessy.Game.Values.Cell.Unit.Stats;
using System;

namespace Chessy.Game.System.Model
{
    public struct AttackUnitS
    {
        public void AttackUnit(in float damage, in PlayerTypes whoKiller, in byte idx_0, in SystemsModelGame systemsModel, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            if (damage <= 0) throw new Exception();
            if (!e.CellEs(idx_0).IsActiveParentSelf) throw new Exception();


            ref var health = ref e.UnitHpC(idx_0).Health;

            health -= damage;
            if (health <= HpValues.HP_FOR_DEATH_AFTER_ATTACK)
                systemsModel.KillUnitS.Kill(idx_0, whoKiller, systemsModel.SetLastDiedS, e);
        }
    }
}