namespace Game.Game
{
    public struct SelIdx
    {
        private static byte _idx;
        public static byte Idx => _idx;
        public static bool IsSelCell => _idx != 0;

        public SelIdx(byte idx)
        {
            _idx = idx;
        }

        public static void Set(byte idx)
        {
            _idx = idx;
        }
        public static void Reset()
        {
            _idx = 0;
        }
    }
}