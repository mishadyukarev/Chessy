using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using System;

namespace Chessy.Game.Model.System
{
    sealed class AttackShieldS : SystemModelGameAbs
    {
        internal AttackShieldS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Attack(in float damage, in byte cell_0)
        {
            if (damage <= 0) throw new Exception();

            eMG.UnitExtraProtectionC(cell_0).Protection -= damage;
            if (!eMG.UnitExtraProtectionC(cell_0).HaveAnyProtection)
                eMG.UnitExtraTWTC(cell_0).ToolWeaponT = ToolWeaponTypes.None;
        }
    }
}