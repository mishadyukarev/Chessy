using ECS;

namespace Game.Game
{
    public struct UnitInfoE
    {
        public bool HaveInInventor;
        public int UnitsInGameC;
        public CooldownC ScoutHeroCooldownC;

        internal UnitInfoE(bool haveInInventor) : this()
        {
            HaveInInventor = haveInInventor;
        }
    }
}