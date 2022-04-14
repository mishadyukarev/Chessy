using Chessy.Game.Model.Entity;
using System;

namespace Chessy.Game.Model.System
{
    sealed class AttackShieldUnitOnCellS : SystemModel
    {
        internal AttackShieldUnitOnCellS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Attack(in float damage, in byte cell_0)
        {
            if (damage <= 0) throw new Exception();

            eMG.ExtraTWProtectionC(cell_0).Protection -= damage;
            if (!eMG.ExtraTWProtectionC(cell_0).HaveAnyProtection)
                eMG.ExtraToolWeaponTC(cell_0).ToolWeaponT = ToolWeaponTypes.None;
        }
    }
}