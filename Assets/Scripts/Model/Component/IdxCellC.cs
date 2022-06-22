namespace Chessy.Game
{
    public struct IdxCellC
    {
        public byte Idx { get; internal set; }

        internal IdxCellC(in byte idx) => Idx = idx;
    }
}