namespace Chessy.Game
{
    public struct TWDoingMC
    {
        private static ToolWeaponTypes _tW;
        private static LevelTWTypes _levelTW;

        public static void Set(ToolWeaponTypes tw, LevelTWTypes level)
        {
            _tW = tw;
            _levelTW = level;
        }

        public static void Get(out ToolWeaponTypes tw, out LevelTWTypes level)
        {
            tw = _tW;
            level = _levelTW;
        }
    }
}
