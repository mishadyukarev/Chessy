namespace Chessy.Model.Component
{
    public struct WhereViewIdxCellC
    {
        public byte ViewIdxCell { get; internal set; }
        public byte DataIdxCell { get; internal set; }

        public bool HaveViewReference => ViewIdxCell != 0;
        public bool HaveDataReference => DataIdxCell != 0;
    }
}