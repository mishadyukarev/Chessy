namespace Chessy.Game
{
    public struct FireC
    {
        public bool HaveFire { get; set; }
        public bool DisableFire() => HaveFire = default;
        public bool EnabFire() => HaveFire = true;
    }
}
