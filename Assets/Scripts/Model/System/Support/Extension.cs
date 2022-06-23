﻿using Chessy.Model;
using System;

namespace Chessy
{
    public static class Extension
    {
        public static bool IsGod(this UnitTypes unitT)
        {
            switch (unitT)
            {
                case UnitTypes.Elfemale: return true;
                case UnitTypes.Snowy: return true;
                case UnitTypes.Undead: return true;
                case UnitTypes.Hell: return true;
                default: return false;
            }
        }
        public static bool Is(this UnitTypes unitT, params UnitTypes[] units)
        {
            if (units == default) throw new Exception();

            foreach (var unit in units) if (unit == unitT) return true;
            return false;
        }
        public static bool HaveUnit(this UnitTypes unitT) => !Is(unitT, UnitTypes.None, UnitTypes.End);
        public static bool IsAnimal(this UnitTypes unitT) => unitT == UnitTypes.Wolf;
        public static bool IsMelee(this UnitTypes unitT, in ToolWeaponTypes mainTW)
        {
            switch (unitT)
            {
                case UnitTypes.Pawn:
                    if (mainTW == ToolWeaponTypes.BowCrossbow) return false;
                    break;

                case UnitTypes.Elfemale:
                    return false;

                case UnitTypes.Snowy:
                    return false;
            }
            return true;
        }
    }
}