namespace Chessy.Game
{
    public struct FromToDoingMC
    {
        private static byte _fromIdx;
        private static byte _toIdx;

        public static void Set(byte fromIdx, byte toIdx)
        {
            _fromIdx = fromIdx;
            _toIdx = toIdx;
        }
        public static void Get(out byte fromIdx, out byte toIdx)
        {
            fromIdx = _fromIdx;
            toIdx = _toIdx;
        }
    }
}
