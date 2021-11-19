namespace Game.Game
{
    public struct SelIdx
    {
        private static byte _idx;
        public static byte Idx => _idx;

        public SelIdx(byte idx)
        {
            _idx = idx;
        }

        public static void Set(byte idx)
        {
            _idx = idx;
        }
    }
}