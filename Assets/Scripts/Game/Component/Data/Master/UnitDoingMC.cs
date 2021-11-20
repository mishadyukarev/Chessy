namespace Game.Game
{
    public struct UnitDoingMC
    {
        private static UnitTypes _unit;

        public static void Set(UnitTypes unit)
        {
            _unit = unit;
        }
        public static void Get(out UnitTypes unit)
        {
            unit = _unit;
        }
    }
}