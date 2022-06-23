namespace Chessy.Model.Model.Entity
{
    public struct GodInfoE
    {
        public bool HaveHeroInInventor;
        public UnitTypes UnitT { get; internal set; }
        public CooldownC CooldownC;

        public float Cooldown
        {
            get => CooldownC.Cooldown;
            set => CooldownC.Cooldown = value;
        }
    }
}