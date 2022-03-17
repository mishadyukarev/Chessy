using Chessy.Game.Values.Cell.Unit.Stats;
using System;

namespace Chessy.Game.System.Model
{
    public struct AttackUnitS
    {
        public void AttackUnit(in float damage, in PlayerTypes whoKiller, in byte idx_to, in SystemsModelManager sMM, in EntitiesModel e)
        {
            if (damage <= 0) throw new Exception();


            e.UnitHpC(idx_to).Health -= damage;
            if (e.UnitHpC(idx_to).Health <= HpValues.HP_FOR_DEATH_AFTER_ATTACK)
                e.UnitHpC(idx_to).Health = 0;

            if (!e.UnitHpC(idx_to).IsAlive)
            {
                sMM.KillUnitS.Kill(idx_to, whoKiller, e);
            }
        }
    }
}