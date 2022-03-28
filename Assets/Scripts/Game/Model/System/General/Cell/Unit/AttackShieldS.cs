using Chessy.Game.Entity.Model;
using Chessy.Game.Entity.Model.Cell.Unit;
using System;

namespace Chessy.Game.System.Model
{
    sealed class AttackShieldS
    {
        readonly ExtraToolWeaponE _extraTWE;

        internal AttackShieldS(in ExtraToolWeaponE extraTWE)
        {
            _extraTWE = extraTWE;
        }

        internal void Attack(in float damage)
        {
            if (damage <= 0) throw new Exception();

            _extraTWE.ProtectionC.Protection -= damage;
            if (!_extraTWE.ProtectionC.HaveAnyProtection)
                _extraTWE.ToolWeaponTC.ToolWeapon = ToolWeaponTypes.None;
        }
    }
}