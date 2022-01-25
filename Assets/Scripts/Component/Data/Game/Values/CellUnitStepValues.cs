using System;

namespace Game.Game
{
    public static class CellUnitStepValues
    {
        public static int NeedAmountSteps(EnvironmentTypes env)
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
        public static int MaxAmountSteps(in UnitTypes unit, in bool haveEffect/*, in int upgradeSteps*/)
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
                case UnitTypes.Snowy: steps = 3; break;
                case UnitTypes.Camel: steps = 3; break;
                default: throw new Exception();
            }

            if (haveEffect) steps += 1;

            //steps += upgradeSteps;

            return steps;
        }
        public static int NeedSteps(in UniqueAbilityTypes uniq)
        {
            switch (uniq)
            {
                case UniqueAbilityTypes.CircularAttack: return 1;
                case UniqueAbilityTypes.BonusNear: return 1;
                case UniqueAbilityTypes.FirePawn: return 1;
                case UniqueAbilityTypes.PutOutFirePawn: return 1;
                case UniqueAbilityTypes.Seed: return 1;
                case UniqueAbilityTypes.FireArcher: return 2;
                case UniqueAbilityTypes.ChangeCornerArcher: return 1;
                case UniqueAbilityTypes.GrowAdultForest: return 1;
                case UniqueAbilityTypes.StunElfemale: return 1;
                case UniqueAbilityTypes.ChangeDirectionWind: return 1;
                case UniqueAbilityTypes.IceWall: return 1;
                default: throw new Exception();
            }
        }
        public static int NeedSteps(in BuildingTypes build)
        {
            return 1;
        }
    }
}