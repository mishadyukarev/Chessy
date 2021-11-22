namespace Game.Game
{
    public struct CloudCenterC
    {
        public static byte Idx { get; set; }

        public static void Sync(byte idx)
        {
            Idx = idx;
        }
    }
}