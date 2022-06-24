namespace Chessy.Model
{
    public struct IdxCellC
    {
        public readonly byte Idx;

        internal IdxCellC(in byte idx) => Idx = idx;
    }
}