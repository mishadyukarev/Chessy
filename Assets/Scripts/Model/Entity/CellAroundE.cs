namespace Chessy.Model
{
    public struct CellAroundE
    {
        public readonly IdxCellC IdxC;
        public readonly XyCellC XyC;

        internal CellAroundE(in byte cell, in byte[] xy)
        {
            IdxC = new IdxCellC(cell);
            XyC = new XyCellC(xy);
        }
    }
}