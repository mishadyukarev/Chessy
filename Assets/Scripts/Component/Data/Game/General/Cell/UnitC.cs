using System;

namespace Game.Game
{
    public struct UnitC : IUnitCellE
    {
        public UnitTypes Unit { get; internal set; }

        public bool Have => Unit != UnitTypes.None;
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
                    default: throw new Exception();
                }
            }
        }
        public bool Is(params UnitTypes[] units)
        {
            foreach (var unit in units)
                if (unit == Unit) return true;
            return false;
        }
        public bool IsHero
        {
            get
            {
                switch (Unit)
                {
                    case UnitTypes.None: throw new Exception();
                    case UnitTypes.King: return false;
                    case UnitTypes.Pawn: return false;
                    case UnitTypes.Archer: return false;
                    case UnitTypes.Scout: return false;
                    case UnitTypes.Elfemale: return true;
                    default: throw new Exception();
                }
            }
        }
        public int CostFood
        {
            get
            {
                if (Have)
                {
                    if (!Is(UnitTypes.King)) return 10;
                    return 0;
                }

                return 0;
            }
        }

        internal UnitC(in UnitTypes unit) => Unit = unit;

        internal void Reset() => Unit = UnitTypes.None;
    }
}
