using System;

namespace Game.Game
{
    public static class ScoutHeroCooldownValues
    {
        internal static int AfterKill(in UnitTypes unitT)
        {
            switch (unitT)
            {
                case UnitTypes.Scout: return 3;
                case UnitTypes.Elfemale: return 5;
                case UnitTypes.Snowy: return 5;
                case UnitTypes.Undead: return 5;
                case UnitTypes.Hell: return 5;
                default: throw new Exception();
            }
        }
    }
}