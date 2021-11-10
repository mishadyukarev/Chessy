namespace Chessy.Game
{
    public struct SelUnitC
    {
        private static UnitTypes _selUnit;
        private static LevelUnitTypes _levelSelUnit;

        public static UnitTypes SelUnit => _selUnit;
        public static LevelUnitTypes LevelSelUnit => _levelSelUnit;
        public static bool IsSelUnit => SelUnit != default;


        public static void SetSelUnit(UnitTypes unit, LevelUnitTypes level)
        {
            _selUnit = unit;
            _levelSelUnit = level;
        }
        public static void ResetSelUnit()
        {
            _selUnit = default;
            _levelSelUnit = default;
        }
    }
}