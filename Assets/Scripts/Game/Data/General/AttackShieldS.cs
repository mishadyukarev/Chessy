namespace Chessy.Game.System.Model
{
    sealed class AttackShieldS : CellSystem, IEcsRunSystem
    {
        internal AttackShieldS(in byte idx, in EntitiesModel eM) : base(idx, eM)
        {
        }

        public void Run()
        {
            var damage = E.UnitExtraTWE(Idx).DamageBrokeShieldC.Damage;

            if (damage > 0)
            {
                E.UnitExtraProtectionTC(Idx).Protection -= damage;
                if (!E.UnitExtraProtectionTC(Idx).HaveAnyProtection)
                    E.UnitExtraTWTC(Idx).ToolWeapon = ToolWeaponTypes.None;


                E.UnitExtraTWE(Idx).DamageBrokeShieldC.Damage = 0;
            }
        }
    }
}