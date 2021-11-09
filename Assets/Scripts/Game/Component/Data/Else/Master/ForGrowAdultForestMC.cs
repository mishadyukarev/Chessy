namespace Chessy.Game
{
    public struct ForGrowAdultForestMC
    {
        private static byte _idxCell;

        public static void Set(byte idxCell) => _idxCell = idxCell;
        public static void Get(out byte idxCell) => idxCell = _idxCell;
    }
}