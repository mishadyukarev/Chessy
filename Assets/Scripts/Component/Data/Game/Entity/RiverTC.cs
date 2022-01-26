namespace Game.Game
{
    public struct RiverTC
    {
        public RiverTypes River;

        public bool HaveRiver => River != default;

        public RiverTC(in RiverTypes river) => River = river;
    }
}