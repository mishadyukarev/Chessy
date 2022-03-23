namespace Chessy.Game
{
    public struct CooldownC
    {
        public float Cooldown;

        public bool HaveCooldown => Cooldown > 0;
    }
}