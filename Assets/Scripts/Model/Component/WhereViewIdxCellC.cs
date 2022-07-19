namespace Chessy.Model.Component
{
    public struct WhereViewIdxCellC
    {
        internal byte ViewIdxCell;
        internal byte DataIdxCell;

        public byte ViewIdxCellP => ViewIdxCell;
        public byte DataIdxCellP => DataIdxCell;

        public bool HaveViewReference => ViewIdxCell != 0;
        public bool HaveDataReference => DataIdxCell != 0;
    }
}