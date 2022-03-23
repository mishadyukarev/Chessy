using System;

namespace Chessy.Game.System.Model
{
    public struct AttackShieldS
    {
        public void Attack(in float damage, in byte idx_0, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            if (damage <= 0) throw new Exception();

            e.UnitExtraProtectionTC(idx_0).Protection -= damage;
            if (!e.UnitExtraProtectionTC(idx_0).HaveAnyProtection)
                e.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;
        }
    }
}