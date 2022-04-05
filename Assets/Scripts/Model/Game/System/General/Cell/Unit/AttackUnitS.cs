﻿using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit.Stats;
using System;

namespace Chessy.Game.Model.System
{
    sealed class AttackUnitS : SystemModelGameAbs
    {
        internal AttackUnitS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Attack(in float damage, in PlayerTypes whoKiller, in byte cell_0)
        {
            if (damage <= 0) throw new Exception();
            if (!eMG.IsActiveParentSelf(cell_0)) throw new Exception();

            eMG.HpUnitC(cell_0).Health -= damage;
            if (eMG.HpUnitC(cell_0).Health <= HpValues.HP_FOR_DEATH_AFTER_ATTACK)
                sMG.UnitSs.KillUnitS.Kill(whoKiller, cell_0);
        }
    }
}