using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit.Stats;
using System;

namespace Chessy.Game.Model.System
{
    sealed class AttackUnitS : SystemModel
    {
        internal AttackUnitS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Attack(in double damage, in PlayerTypes whoKiller, in byte cellIdx)
        {
            //if (!eMG.UnitTC(cellIdx).HaveUnit) throw new Exception();
            if (damage <= 0) throw new Exception();
            if (eMG.IsBorder(cellIdx)) throw new Exception();

            eMG.HpUnitC(cellIdx).Health -= damage;
            if (eMG.HpUnitC(cellIdx).Health <= HpValues.HP_FOR_DEATH_AFTER_ATTACK)
                sMG.UnitSs.KillUnitS.Kill(whoKiller, cellIdx);
        }
    }
}