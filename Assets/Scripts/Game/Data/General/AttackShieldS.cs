namespace Game.Game
{
    sealed class AttackShieldS : SystemAbstract
    {
        internal AttackShieldS(in EntitiesModel ents) : base(ents)
        {
            E.AttackShieldE = new AttackShieldE(Attack);
        }

        void Attack(float damage, byte idx_0)
        {
            E.UnitExtraProtectionTC(idx_0).Protection -= damage;
            if (!E.UnitExtraProtectionTC(idx_0).HaveAnyProtection)
                E.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;
        }
    }
}