using System;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitSystems
    {
        internal void AttackShield(in float damage, in byte cell_0)
        {
            if (damage <= 0) throw new Exception();

            _eMG.ExtraTWProtectionC(cell_0).Protection -= damage;
            if (!_eMG.ExtraTWProtectionC(cell_0).HaveAnyProtection)
                _eMG.ExtraToolWeaponTC(cell_0).ToolWeaponT = ToolWeaponTypes.None;
        }
    }
}