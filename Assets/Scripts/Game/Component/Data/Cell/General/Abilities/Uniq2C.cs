using System;

namespace Scripts.Game
{
    public struct Uniq2C
    {
        public UniqAbilTypes Ability { get; private set; }
        public int Cooldown { get; set; }

        public void SetAbility(UnitTypes unit)
        {
            switch (unit)
            {
                case UnitTypes.None:
                    Ability = default;
                    break;

                case UnitTypes.King:
                    Ability = UniqAbilTypes.BonusNear;
                    break;

                case UnitTypes.Pawn:
                    Ability = default;
                    break;

                case UnitTypes.Rook:
                    Ability = default;
                    break;

                case UnitTypes.Bishop:
                    Ability = default;
                    break;

                case UnitTypes.Scout:
                    Ability = default;
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