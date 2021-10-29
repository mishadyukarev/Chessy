using System;

namespace Scripts.Game
{
    public struct CellUnitDataCom
    {
        public UnitTypes UnitType;
        public bool HaveUnit => UnitType != UnitTypes.None;
        public void DefUnitType() => UnitType = default;
        public bool Is(UnitTypes unitType) => UnitType.Is(unitType);
        public bool Is(UnitTypes[] unitTypes) => UnitType.Is(unitTypes);
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

        public LevelUnitTypes LevelUnitType;


        public void Set(CellUnitDataCom newUnit)
        {
            UnitType = newUnit.UnitType;
            LevelUnitType = newUnit.LevelUnitType;
        }
    }
}
