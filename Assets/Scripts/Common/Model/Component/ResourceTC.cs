namespace Chessy.Game
{
    public struct ResourceTC
    {
        public ResourceTypes Resource;

        public ResourceTC(in ResourceTypes resT) => Resource = resT;
    }
}