namespace Chessy.Model
{
    public sealed class IdxCellC
    {
        public readonly byte Idx;

        internal IdxCellC(in byte idx) => Idx = idx;
    }
}