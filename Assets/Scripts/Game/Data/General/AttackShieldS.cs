using System;

namespace Game.Game
{
    sealed class AttackShieldS : SystemAbstract, IEcsRunSystem
    {
        internal AttackShieldS(in EntitiesModel ents) : base(ents)
        {

        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Start_Values.ALL_CELLS_AMOUNT; idx_0++)
            {
                var damage = E.UnitExtraTWE(idx_0).DamageBrokeShieldC.Damage;

                if (damage > 0)
                {
                    E.UnitExtraProtectionTC(idx_0).Protection -= damage;
                    if (!E.UnitExtraProtectionTC(idx_0).HaveAnyProtection)
                        E.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;


                    E.UnitExtraTWE(idx_0).DamageBrokeShieldC.Damage = 0;
                }
            }
        }
    }
}