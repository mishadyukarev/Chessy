namespace Game.Game
{
    public struct CooldownC
    {
        public int Cooldown;

        public bool HaveCooldown => Cooldown > 0;
    }
}