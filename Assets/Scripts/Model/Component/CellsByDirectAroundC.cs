namespace Chessy.Model.Component
{
    public sealed class CellsByDirectAroundC
    {
        public readonly byte[] CellsByDirect;

        public byte Get(in DirectTypes directT) => CellsByDirect[(byte)directT];

        internal CellsByDirectAroundC(in byte[] cellsByDirec)
        {
            CellsByDirect = cellsByDirec;
        }
    }
}