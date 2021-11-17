namespace Chessy.Game
{
    public struct SelUnitC
    {
        private static UnitTypes _selUnit;
        private static LevelTypes _levelSelUnit;

        public static UnitTypes SelUnit => _selUnit;
        public static LevelTypes LevelSelUnit => _levelSelUnit;
        public static bool IsSelUnit => SelUnit != default;


        public static void SetSelUnit(UnitTypes unit, LevelTypes level)
        {
            _selUnit = unit;
            _levelSelUnit = level;
        }
        //public static void Reset()
        //{
        //    _selUnit = default;
        //    _levelSelUnit = default;
        //}
    }
}