namespace Chessy.Model.Cell.Unit
{
    public struct EffectsUnitC
    {
        public float StunHowManyUpdatesNeedStay { get; internal set; }
        public float ProtectionRainyMagicShield { get; internal set; }
        public bool HaveFrozenArrawArcher { get; internal set; }
        public int SecondForSnowyFrozenArraw { get; internal set; }

        public bool HaveAnyProtectionRainyMagicShield => ProtectionRainyMagicShield >= 1;
        public bool IsStunned => StunHowManyUpdatesNeedStay >= 1;
    }
}