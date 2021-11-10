namespace Chessy.Game
{
    public struct IdxDoingMC
    {
        private static byte _idxCell;

        public static void Set(byte idx)
        {
            _idxCell = idx;
        }
        public static void Get(out byte idx)
        {
            idx = _idxCell;
        }
    }
}