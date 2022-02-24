using System;

namespace Game.Game
{
    public struct PlayerLevelUnitInfoE
    {
        public bool HaveInInventor;
        public int UnitsInGame;
        public CooldownC ScoutHeroCooldownC;

        public float MaxSteps;
        public float MaxWater;

        public bool HaveCenterUpgrade;

        internal PlayerLevelUnitInfoE(in LevelTypes levT, in UnitTypes unitT, bool haveInInventor) : this()
        {
            HaveInInventor = haveInInventor;

            var maxSteps = 0f;

            switch (unitT)
            {
                case UnitTypes.King: maxSteps = 1; break;
                case UnitTypes.Pawn: maxSteps = 1; break;

                case UnitTypes.Scout: maxSteps = 2.5f; break;

                case UnitTypes.Elfemale: maxSteps = 1.5f; break;
                case UnitTypes.Snowy: maxSteps = 1.5f; break;
                case UnitTypes.Undead: maxSteps = 2; break;
                case UnitTypes.Hell: maxSteps = 1; break;

                case UnitTypes.Skeleton: maxSteps = 2; break;

                case UnitTypes.Camel: maxSteps = 2; break;

                default: throw new Exception();
            }

            MaxSteps = maxSteps;


            MaxWater = UnitWater_Values.MAX;
        }
    }
}