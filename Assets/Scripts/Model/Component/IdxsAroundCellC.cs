namespace Chessy.Model.Component
{
    public sealed class IdxsAroundCellC
    {
        readonly byte[] _idxCellsAroundArray;

        public byte[] IdxCellsAroundArray => (byte[])_idxCellsAroundArray.Clone();

        internal IdxsAroundCellC(in byte[] cellsAround)
        {
            _idxCellsAroundArray = cellsAround;
        }
    }
}