namespace Game.Game
{
    sealed class AttackShieldS : SystemAbstract, IEcsRunSystem
    {
        internal AttackShieldS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            var idx_0 = E.AttackShieldE.IdxC.Idx;

            E.UnitExtraProtectionTC(idx_0).Protection -= E.AttackShieldE.DamageC.Damage;
            if (!E.UnitExtraProtectionTC(idx_0).HaveAnyProtection)
                E.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;
        }
    }
}