namespace Chessy.Game.Model.Entity
{
    public struct GodInfoE
    {
        public bool HaveHeroInInventor;
        public UnitTC UnitTC;
        public CooldownC CooldownC;

        public UnitTypes UnitT
        {
            get => UnitTC.UnitT;
            set => UnitTC.UnitT = value;
        }
        public float Cooldown
        {
            get => CooldownC.Cooldown;
            set => CooldownC.Cooldown = value;
        }
    }
}