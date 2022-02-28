namespace Chessy.Game
{
    public struct IdxFromToC
    {
        public byte From;
        public byte To;

        public void Set(in byte fromIdx, in byte toIdx)
        {
            From = fromIdx;
            To = toIdx;
        }
        public void Get(out byte fromIdx, out byte toIdx)
        {
            fromIdx = From;
            toIdx = To;
        }
    }
}