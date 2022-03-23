using System;

namespace Chessy.Game
{
    public struct UnitTC
    {
        UnitTypes _unitT;

        public UnitTypes Unit
        {
            get => _unitT;
            set => _unitT = value;
        }

        public bool Is(params UnitTypes[] units)
        {
            if (units == default) throw new Exception();

            foreach (var unit in units) if (unit == Unit) return true;
            return false;
        }

        public bool IsHero
        {
            get
            {
                switch (Unit)
                {
                    case UnitTypes.Elfemale: return true;
                    case UnitTypes.Snowy: return true;
                    case UnitTypes.Undead: return true;
                    case UnitTypes.Hell: return true;
                    default: return false;
                }
            }
        }
        
        public bool HaveUnit => !Is(UnitTypes.None, UnitTypes.End); 

        public UnitTC(in UnitTypes unit) => _unitT = unit;

        public void ClearUnit() => Unit = UnitTypes.None;
    }
}
