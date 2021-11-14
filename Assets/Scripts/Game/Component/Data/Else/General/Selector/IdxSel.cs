namespace Chessy.Game
{
    public struct IdxSel
    {
        public static byte Idx { get; set; }


        public static bool IsSelCell => Idx != default;
        public static void Reset() => Idx = 0;

        public IdxSel(byte idx)
        {
            Idx = idx;
        }
    }
}