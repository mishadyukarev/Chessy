namespace Chessy.Game
{
    public struct UnitDoingMC
    {
        private static UnitTypes _unitType;

        public static void Set(UnitTypes unitType)
        {
            _unitType = unitType;
        }
        public static void Get(out UnitTypes unitType)
        {
            unitType = _unitType;
        }
    }
}