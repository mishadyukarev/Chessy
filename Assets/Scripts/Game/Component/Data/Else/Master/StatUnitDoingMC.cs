namespace Chessy.Game
{
    public struct StatUnitDoingMC
    {
        private static UnitStatTypes _stat;

        public static void Set(UnitStatTypes stat)
        {
            _stat = stat;
        }
        public static void Get(out UnitStatTypes stat)
        {
            stat = _stat;
        }
    }
}