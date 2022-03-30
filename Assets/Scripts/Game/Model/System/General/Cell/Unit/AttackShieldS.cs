using Chessy.Game.Entity.Model;
using System;

namespace Chessy.Game.System.Model
{
    sealed class AttackShieldS : SystemModelGameAbs
    {
        internal AttackShieldS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Attack(in float damage, in byte cell_0)
        {
            if (damage <= 0) throw new Exception();

            e.UnitExtraProtectionC(cell_0).Protection -= damage;
            if (!e.UnitExtraProtectionC(cell_0).HaveAnyProtection)
                e.UnitExtraTWTC(cell_0).ToolWeapon = ToolWeaponTypes.None;
        }
    }
}