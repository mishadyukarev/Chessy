using System;

namespace Game.Game
{
    internal readonly struct StepUnitValues
    {
        internal int NeedAmountSteps(EnvironmentTypes env)
        {
            switch (env)
            {
                case EnvironmentTypes.None: throw new Exception();
                case EnvironmentTypes.Fertilizer: throw new Exception();
                case EnvironmentTypes.YoungForest: throw new Exception();
                case EnvironmentTypes.AdultForest: return 1;
                case EnvironmentTypes.Hill: return 1;
                case EnvironmentTypes.Mountain: throw new Exception();
                default: throw new Exception();
            }
        }
        internal int MaxAmountSteps(in UnitTypes unit, in bool haveEffect/*, in int upgradeSteps*/)
        {
            var steps = 0;

            switch (unit)
            {
                case UnitTypes.None: steps = 0; break;
                case UnitTypes.King: steps = 2; break;
                case UnitTypes.Pawn: steps = 2; break;
                case UnitTypes.Archer: steps = 3; break;
                case UnitTypes.Scout: steps = 5; break;
                case UnitTypes.Elfemale: steps = 3; break;
                default: throw new Exception();
            }

            if (haveEffect) steps += 1;

            //steps += upgradeSteps;

            return steps;
        }
    }
}