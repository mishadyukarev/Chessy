namespace Chessy.Model
{
    public struct GodInfoC
    {
        public bool HaveGodInInventor { get; internal set; }
        public UnitTypes UnitT { get; internal set; }
        public int CooldownInSecondsForNextAppearance { get; internal set; }

        public bool HaveCooldown => CooldownInSecondsForNextAppearance >= 1;
    }
}