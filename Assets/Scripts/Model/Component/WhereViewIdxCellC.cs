namespace Chessy.Model.Component
{
    public sealed class WhereViewIdxCellC
    {
        internal byte ViewIdxCell;
        internal byte DataIdxCell;

        public byte ViewIdxCellP => ViewIdxCell;
        public byte DataIdxCellP => DataIdxCell;

        public bool HaveViewReference => ViewIdxCell != 0;
        public bool HaveDataReference => DataIdxCell != 0;

        internal void Dispose()
        {
            ViewIdxCell = default;
            DataIdxCell = default;
        }
        internal void Clone(in WhereViewIdxCellC whereViewIdxCellC)
        {
            ViewIdxCell = whereViewIdxCellC.ViewIdxCell;
            DataIdxCell = whereViewIdxCellC.DataIdxCell;
        }
    }
}