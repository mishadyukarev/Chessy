namespace Game.Game
{
    public struct RiverTC
    {
        public RiverTypes River;

        public RiverTC(in RiverTypes river) => River = river;
    }
}