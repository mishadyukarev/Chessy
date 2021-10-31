using System;

namespace Scripts.Game
{
    public struct CellUnitDataCom
    {
        private UnitTypes _unitType;

        public UnitTypes UnitType => _unitType;
        public bool HaveUnit => UnitType != UnitTypes.None;
        public bool IsMelee
        {
            get
            {
                switch (UnitType)
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
            if (HaveUnit) throw new Exception("It's got unit");

            _unitType = unitType;
        }
        public void NoneUnit()
        {
            if (!HaveUnit) throw new Exception("It's not got unit");

            _unitType = UnitTypes.None;
        }
        public void Sync(UnitTypes unitType)
        {
            _unitType = unitType;
        }

        public bool Is(UnitTypes unitType) => UnitType.Is(unitType);
        public bool Is(UnitTypes[] unitTypes) => UnitType.Is(unitTypes);
    }
}
