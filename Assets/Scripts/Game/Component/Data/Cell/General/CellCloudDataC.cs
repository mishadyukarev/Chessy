namespace Chessy.Game
{
    public struct CellCloudDataC
    {
        public bool HaveCloud;
        public CloudWidthTypes CloudWidthType { get; set; }
        public bool IsCenter => CloudWidthType != default;
    }
}