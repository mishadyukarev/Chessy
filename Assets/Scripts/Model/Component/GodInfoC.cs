namespace Chessy.Model
{
    public struct GodInfoC
    {
        public bool HaveGodInInventor;
        public UnitTypes UnitT { get; internal set; }
        public int CooldownInSecondsForNextAppearance { get; set; }

        public bool HaveCooldown => CooldownInSecondsForNextAppearance >= 1;
    }
}