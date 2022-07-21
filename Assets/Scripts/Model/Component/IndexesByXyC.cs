namespace Chessy.Model.Component
{
    public sealed class IndexesByXyC
    {
        public readonly byte[,] Idxs;

        public byte GetIdxCellByXy(params byte[] xy) => Idxs[xy[0], xy[1]];

        internal IndexesByXyC(in byte[,] idxs)
        {
            Idxs = idxs;
        }
    }
}