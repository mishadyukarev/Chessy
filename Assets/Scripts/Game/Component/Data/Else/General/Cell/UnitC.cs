﻿using System;

namespace Game.Game
{
    public struct UnitC
    {
        private UnitTypes _unit;

        public UnitTypes Unit => _unit;
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
                if (unit == _unit) return true;
            return false;
        }
        public bool IsHero
        {
            get
            {
                switch (_unit)
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



        public void Set(UnitTypes unit)
        {
            if (unit == UnitTypes.None) throw new Exception();
            if (HaveUnit) throw new Exception("It's got unit");

            _unit = unit;
        }
        public void Reset()
        {
            if (!HaveUnit) throw new Exception("It's not got unit");

            _unit = UnitTypes.None;
        }
        public void Sync(UnitTypes unitType)
        {
            _unit = unitType;
        }
    }
}
