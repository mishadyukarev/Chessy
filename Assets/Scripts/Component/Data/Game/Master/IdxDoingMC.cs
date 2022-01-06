namespace Game.Game
{
    public struct IdxDoingMC
    {
        static byte _idxCell;

        public static void Set(in byte idx)
        {
            _idxCell = idx;
        }
        public static void Get(out byte idx)
        {
            idx = _idxCell;
        }
    }
}