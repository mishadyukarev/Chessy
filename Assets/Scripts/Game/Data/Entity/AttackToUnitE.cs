namespace Game.Game
{
    public struct AttackToUnitE
    {
        public DamageC AttackDamageC;
        public PlayerTC WhoKillerC;
        public IdxC FromIdx;

        public void Set(in float damage, in PlayerTypes killer, in byte idx = 0)
        {
            AttackDamageC.Damage = damage;
            WhoKillerC.Player = killer;
            FromIdx.Idx = idx;
        }
    }
}