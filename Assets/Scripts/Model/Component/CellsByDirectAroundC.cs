namespace Chessy.Model.Component
{
    public sealed class CellsByDirectAroundC
    {
        readonly byte[] _cellsByDirect;
        public byte[] CellsByDirect => (byte[])_cellsByDirect.Clone();

        public byte Get(in DirectTypes directT) => _cellsByDirect[(byte)directT];

        internal CellsByDirectAroundC(in byte[] cellsByDirec)
        {
            _cellsByDirect = cellsByDirec;
        }
    }
}