using System;

namespace Chessy.Game
{
    public struct Uniq1C
    {
        public UniqAbilTypes Ability { get; private set; }
        public int Cooldown { get; set; }

        public void SetAbility(UnitTypes unit, CellEnvDataC envC, CellFireDataC fireC)
        {
            switch (unit)
            {
                case UnitTypes.None: throw new Exception();

                case UnitTypes.King:
                    Ability = UniqAbilTypes.CircularAttack;
                    break;

                case UnitTypes.Pawn:
                    if (envC.Have(EnvTypes.AdultForest))
                    {
                        if (fireC.HaveFire) Ability = UniqAbilTypes.NoneFirePawn;
                        else Ability = UniqAbilTypes.FirePawn;
                    }
                    else
                    {
                        Ability = UniqAbilTypes.Seed;
                    }
                    break;

                case UnitTypes.Rook:
                    Ability = UniqAbilTypes.FireArcher;
                    break;

                case UnitTypes.Bishop:
                    Ability = UniqAbilTypes.FireArcher;
                    break;

                case UnitTypes.Scout:
                    Ability = UniqAbilTypes.None;
                    break;

                default: throw new Exception();
            }
        }

        public void Reset()
        {
            Ability = default;
        }
    }
}