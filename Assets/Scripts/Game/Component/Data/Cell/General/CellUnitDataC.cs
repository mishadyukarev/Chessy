using System;

namespace Chessy.Game
{
    public struct CellUnitDataC
    {
        public UnitTypes Unit { get; private set; }
        public bool HaveUnit => Unit != UnitTypes.None;
        public bool IsMelee
        {
            get
            {
                switch (Unit)
                {
                    case UnitTypes.None: throw new Exception();
                    case UnitTypes.King: return true;
                    case UnitTypes.Pawn: return true;
                    case UnitTypes.Rook: return false;
                    case UnitTypes.Bishop: return false;
                    case UnitTypes.Scout: return true;
                    default: throw new Exception();
                }
            }
        }



        public void SetUnit(UnitTypes unitType)
        {
            if (unitType == UnitTypes.None) throw new Exception();
            if (HaveUnit) throw new Exception("It's got unit");

            Unit = unitType;
        }
        public void NoneUnit()
        {
            if (!HaveUnit) throw new Exception("It's not got unit");

            Unit = UnitTypes.None;
        }
        public void Sync(UnitTypes unitType)
        {
            Unit = unitType;
        }

        public bool Is(UnitTypes unitType) => Unit.Is(unitType);
        public bool Is(UnitTypes[] unitTypes) => Unit.Is(unitTypes);
    }
}
