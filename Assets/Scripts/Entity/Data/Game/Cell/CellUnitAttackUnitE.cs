namespace Game.Game
{
    public struct CellUnitAttackUnitE
    {
        public DamageC DamageC;
        public PlayerTC WhoKiller;

        public void Set(in float damage, in PlayerTypes whoKiller)
        {
            DamageC.Damage = damage;
            WhoKiller.Player = whoKiller;
        }
    }
}