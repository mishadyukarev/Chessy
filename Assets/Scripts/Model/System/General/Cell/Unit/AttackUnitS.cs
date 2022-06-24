using Chessy.Model;
using Chessy.Model.Model.Entity;
using Chessy.Model.Values.Cell.Unit.Stats;
using System;

namespace Chessy
{
    static partial class SystemStatic
    {
        internal static void Attack(this EntitiesModel e, in double damage, in PlayerTypes whoKiller, in byte cellIdx)
        {
            //if (!eMG.UnitTC(cellIdx).HaveUnit()) throw new Exception();
            if (damage <= 0) throw new Exception();
            if (e.IsBorder(cellIdx)) throw new Exception();

            e.HpUnitC(cellIdx).Health -= damage;
            if (e.HpUnitC(cellIdx).Health <= HpValues.HP_FOR_DEATH_AFTER_ATTACK)
                e.KillUnit(whoKiller, cellIdx);
        }
    }
}