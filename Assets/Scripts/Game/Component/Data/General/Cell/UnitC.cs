using System;

namespace Game.Game
{
    public struct UnitC
    {
        private UnitTypes _unit;
        private readonly byte _idx;

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



        public UnitC(UnitTypes unit, byte idx)
        {
            _unit = unit;
            _idx = idx;
        }



        public void SetNew(UnitTypes unit, LevelTypes level, PlayerTypes owner)
        {
            if (unit == UnitTypes.None) throw new Exception();
            if (HaveUnit) throw new Exception("It's got unit");
 
            _unit = unit;
            WhereUnitsC.Set(unit, level, owner, _idx, true);
        }

        public void Kill(LevelTypes level, PlayerTypes owner)
        {
            if (!HaveUnit) throw new Exception("It's not got unit");

            if (Is(UnitTypes.King))
            {
                PlyerWinnerC.PlayerWinner = owner;
            }
            else if (Is(new[] { UnitTypes.Scout, UnitTypes.Elfemale }))
            {
                ScoutHeroCooldownC.SetStandCooldown(_unit, owner);
                InvUnitsC.Add(_unit, level, owner);
            }

            WhereUnitsC.Set(_unit, level, owner, _idx, false);
            _unit = UnitTypes.None;
        }

        public void Clean(LevelTypes level, PlayerTypes owner)
        {
            if (!HaveUnit) throw new Exception("It's not got unit");

            WhereUnitsC.Set(_unit, level, owner, _idx, false);
            _unit = UnitTypes.None;
        }

        public void Sync(UnitTypes unitType)
        {
            _unit = unitType;
        }
    }
}
