namespace Chessy.Game
{
    public struct SelIdx
    {
        public static byte Idx { get; set; }


        public static bool IsSelCell => Idx != default;
        public static void Reset() => Idx = 0;

        public SelIdx(byte idx)
        {
            Idx = idx;
        }
    }
}