using System;

namespace Chessy.Model.Model.System
{
    sealed partial class UnitSystems
    {
        internal void AttackShield(in float damage, in byte cell_0)
        {
            if (damage <= 0) throw new Exception();

            _e.ExtraTWProtectionC(cell_0).Protection -= damage;
            if (!_e.ExtraTWProtectionC(cell_0).HaveAnyProtection())
                _e.SetExtraToolWeaponT(cell_0, ToolWeaponTypes.None);
        }
    }
}