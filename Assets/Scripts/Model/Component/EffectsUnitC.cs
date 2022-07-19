namespace Chessy.Model.Cell.Unit
{
    public struct EffectsUnitC
    {
        internal float ProtectionRainyMagicShield;
        internal bool HaveFrozenArrawArcher;

        public float StunHowManyUpdatesNeedStay { get; internal set; }
        public int SecondForSnowyFrozenArraw { get; internal set; }


        public bool HaveFrozenArrawArcherP => HaveFrozenArrawArcher;

        public bool HaveAnyProtectionRainyMagicShield => ProtectionRainyMagicShield >= 1;
        public bool IsStunned => StunHowManyUpdatesNeedStay >= 1;
    }
}