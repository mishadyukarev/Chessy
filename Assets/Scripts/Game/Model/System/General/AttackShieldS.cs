using Chessy.Game.Entity.Model;
using System;

namespace Chessy.Game.System.Model
{
    public sealed class AttackShieldS : SystemModelGameAbs
    {
        public AttackShieldS(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Attack(in float damage, in byte cell_0)
        {
            if (damage <= 0) throw new Exception();

            eMGame.UnitExtraProtectionTC(cell_0).Protection -= damage;
            if (!eMGame.UnitExtraProtectionTC(cell_0).HaveAnyProtection)
                eMGame.UnitExtraTWTC(cell_0).ToolWeapon = ToolWeaponTypes.None;
        }
    }
}