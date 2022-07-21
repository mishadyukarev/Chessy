using System;
namespace Chessy.Model.System
{
    sealed partial class UnitSystems : SystemModelAbstract
    {
        internal void AttackShield(in float damage, in byte cell_0)
        {
            if (damage <= 0) throw new Exception();

            _extraTWC[cell_0].ProtectionShield -= damage;
            if (!_extraTWC[cell_0].HaveAnyProtectionShield)
                _extraTWC[cell_0].Dispose();
        }
    }
}