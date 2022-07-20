namespace Chessy.Model.Component
{
    public sealed class EffectsUnitC
    {
        internal float ProtectionRainyMagicShield;
        internal bool HaveFrozenArrawArcher;
        internal float StunHowManyUpdatesNeedStay;

        public int SecondForSnowyFrozenArraw { get; internal set; }


        public bool HaveFrozenArrawArcherP => HaveFrozenArrawArcher;
        public float StunHowManyUpdatesNeedStayP => StunHowManyUpdatesNeedStay;

        public bool HaveAnyProtectionRainyMagicShield => ProtectionRainyMagicShield >= 1;
        public bool IsStunned => StunHowManyUpdatesNeedStay >= 1;


        internal void Clone(in EffectsUnitC effectsUnitC)
        {
            ProtectionRainyMagicShield = effectsUnitC.ProtectionRainyMagicShield;
            HaveFrozenArrawArcher = effectsUnitC.HaveFrozenArrawArcher;
            StunHowManyUpdatesNeedStay = effectsUnitC.StunHowManyUpdatesNeedStay;
        }

        internal void Set(in float stun, in float protection, in bool haveFrozenArrawArcher)
        {
            StunHowManyUpdatesNeedStay = stun;
            ProtectionRainyMagicShield = protection;
            HaveFrozenArrawArcher = haveFrozenArrawArcher;
        }
    }
}