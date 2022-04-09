namespace Chessy.Game
{
    public struct HealthC
    {
        public double Health { get; internal set; }

        public bool IsAlive => Health > 0;

        internal void Destroy() => Health = 0;
    }
}