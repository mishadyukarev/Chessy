namespace Chessy.Model
{
    public struct GodInfoC
    {
        public bool HaveGodInInventor;
        public UnitTypes UnitT { get; internal set; }
        public float Cooldown { get; set; }

        public bool HaveCooldown => Cooldown >= 1;
    }
}