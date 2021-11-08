namespace Chessy.Game
{
    public struct OldNewC
    {
        private static UnitTypes _unit;

        public static UnitTypes Unit => _unit;
        public static bool Is(UnitTypes unit) => _unit == unit;
        public static void Set(UnitTypes unit) => _unit = unit;
    }
}