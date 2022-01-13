using System;

namespace Game.Game
{
    internal readonly struct StepUnitValues
    {
        internal int NeedAmountSteps(EnvTypes env)
        {
            switch (env)
            {
                case EnvTypes.None: throw new Exception();
                case EnvTypes.Fertilizer: throw new Exception();
                case EnvTypes.YoungForest: throw new Exception();
                case EnvTypes.AdultForest: return 1;
                case EnvTypes.Hill: return 1;
                case EnvTypes.Mountain: throw new Exception();
                default: throw new Exception();
            }
        }
        internal int MaxAmountSteps(UnitTypes unit, bool haveEffect/*, float upg*/)
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

            //steps += (int)upg;

            return steps;
        }
    }
}