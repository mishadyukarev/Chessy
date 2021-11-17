namespace Chessy.Game
{
    public struct TWDoingMC
    {
        private static TWTypes _tW;
        private static LevelTypes _levelTW;

        public static void Set(TWTypes tw, LevelTypes level)
        {
            _tW = tw;
            _levelTW = level;
        }

        public static void Get(out TWTypes tw, out LevelTypes level)
        {
            tw = _tW;
            level = _levelTW;
        }
    }
}
