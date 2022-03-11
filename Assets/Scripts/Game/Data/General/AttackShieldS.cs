using System;

namespace Chessy.Game.System.Model
{
    public struct AttackShieldS
    {
        public AttackShieldS(in float damage, in byte idx_0, in EntitiesModel E)
        {
            if (damage <= 0) throw new Exception();

            E.UnitExtraProtectionTC(idx_0).Protection -= damage;
            if (!E.UnitExtraProtectionTC(idx_0).HaveAnyProtection)
                E.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;
        }
    }
}