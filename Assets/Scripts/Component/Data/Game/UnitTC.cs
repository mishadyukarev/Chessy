using System;

namespace Game.Game
{
    public struct UnitTC
    {
        public UnitTypes Unit;

        public bool Is(params UnitTypes[] units)
        {
            if (units == default) throw new Exception();

            foreach (var unit in units) if (unit == Unit) return true;
            return false;
        }

        public bool IsMelee
        {
            get
            {
                switch (Unit)
                {
                    case UnitTypes.None: throw new Exception();
                    case UnitTypes.King: return true;
                    case UnitTypes.Pawn: return true;
                    case UnitTypes.Archer: return false;
                    case UnitTypes.Scout: return true;

                    case UnitTypes.Elfemale: return false;
                    case UnitTypes.Snowy: return false;
                    case UnitTypes.Undead: return true;
                    case UnitTypes.Hell: return true;

                    case UnitTypes.Skeleton: return true;

                    case UnitTypes.Camel: return false;
                    default: throw new Exception();
                }
            }
        }
        public bool IsHero
        {
            get
            {
                switch (Unit)
                {
                    case UnitTypes.King: return false;
                    case UnitTypes.Pawn: return false;
                    case UnitTypes.Archer: return false;
                    case UnitTypes.Scout: return false;

                    case UnitTypes.Elfemale: return true;
                    case UnitTypes.Snowy: return true;
                    case UnitTypes.Undead: return true;
                    case UnitTypes.Hell: return true;

                    case UnitTypes.Skeleton: return false;

                    case UnitTypes.Camel: return false;
                    default: throw new Exception();
                }
            }
        }
        public bool IsAnimal
        {
            get
            {
                switch (Unit)
                {
                    case UnitTypes.King: return false;

                    case UnitTypes.Pawn: return false;
                    case UnitTypes.Archer: return false;
                    case UnitTypes.Scout: return false;
                    case UnitTypes.Elfemale: return false;

                    case UnitTypes.Snowy: return false;
                    case UnitTypes.Undead: return false;
                    case UnitTypes.Hell: return false;

                    case UnitTypes.Skeleton: return false;

                    case UnitTypes.Camel: return true;
                    default: throw new Exception();
                }
            }
        }

        public UnitTC(in UnitTypes unit) => Unit = unit;
    }
}
