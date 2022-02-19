namespace Game.Game
{
    public struct UnitInfoE
    {
        public bool HaveInInventor;
        public int UnitsInGame;
        public CooldownC ScoutHeroCooldownC;

        internal UnitInfoE(bool haveInInventor) : this()
        {
            HaveInInventor = haveInInventor;
        }
    }
}