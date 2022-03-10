namespace Chessy.Game.System.Model
{
    sealed class AttackShieldS : SystemAbstract, IEcsRunSystem
    {
        internal AttackShieldS(in EntitiesModel eM) : base(eM) { }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
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