namespace Chessy.Game
{
    public struct CooldownC
    {
        public float Cooldown { get; internal set; }

        public bool HaveCooldown => Cooldown > 0;
    }
}