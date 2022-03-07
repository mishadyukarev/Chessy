namespace Chessy.Game
{
    public static class HeroCooldown_VALUES
    {
        public static int AfterKill(in UnitTypes unitT)
        {
            switch (unitT)
            {
                case UnitTypes.Elfemale: return 5;
                case UnitTypes.Snowy: return 5;
                case UnitTypes.Undead: return 1;
                case UnitTypes.Hell: return 10;
                default: return 0;
            }
        }
    }
}