namespace Game.Game
{
    public struct SelectedUnitEnt
    {
        private static UnitTypes _selUnit;
        private static LevelTypes _levelSelUnit;

        public static UnitTypes Unit => _selUnit;
        public static LevelTypes Level => _levelSelUnit;


        public static void SetSelUnit(UnitTypes unit, LevelTypes level)
        {
            _selUnit = unit;
            _levelSelUnit = level;
        }
    }
}