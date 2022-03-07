using System;

namespace Chessy.Game
{
    public struct PlayerLevelUnitInfoE
    {
        public bool HaveInInventor;
        public int UnitsInGame;
        public CooldownC HeroCooldownC;

        public float WaterMax;

        internal PlayerLevelUnitInfoE(in LevelTypes levT, in UnitTypes unitT, bool haveInInventor) : this()
        {
            HaveInInventor = haveInInventor;

            WaterMax = 1;
        }
    }
}