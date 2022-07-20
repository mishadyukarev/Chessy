namespace Chessy.Model
{
    public sealed class GodInfoC
    {
        internal UnitTypes UnitType;
        internal int CooldownInSecondsForNextAppearance;
        internal bool HaveGodInInventor;

        public bool HaveGodInInventorP => HaveGodInInventor;
        public int CooldownInSecondsForNextAppearanceP => CooldownInSecondsForNextAppearance;
        public UnitTypes UnitT => UnitType;
        public bool HaveCooldown => CooldownInSecondsForNextAppearance >= 1;

        internal void Dispose()
        {
            UnitType = default;
            CooldownInSecondsForNextAppearance = default;
            HaveGodInInventor = default;
        }
    }
}