using System;

namespace Chessy.Model
{
    sealed partial class UnitSystems
    {
        internal void AttackShield(in float damage, in byte cell_0)
        {
            if (damage <= 0) throw new Exception();

            _e.UnitExtraTWE(cell_0).ProtectionShield -= damage;
            if (!_e.UnitExtraTWE(cell_0).HaveAnyProtectionShield)
                _e.SetExtraToolWeaponT(cell_0, ToolWeaponTypes.None);
        }
    }
}