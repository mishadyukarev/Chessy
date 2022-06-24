namespace Chessy.Model.Model.Entity.Cell.Unit
{
    public struct EffectsUnitC
    {
        public float StunHowManyUpdatesNeedStay { get; internal set; }
        public float ProtectionRainyMagicShield { get; internal set; }
        public int ShootsFrozenArrawArcher { get; internal set; }
        public bool HaveKingEffect { get; internal set; }

        public bool HaveAnyProtectionRainyMagicShield => ProtectionRainyMagicShield >= 1;
        public bool IsStunned => StunHowManyUpdatesNeedStay >= 1;
        public bool HaveShoots => ShootsFrozenArrawArcher >= 1;
    }
}