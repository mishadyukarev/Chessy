namespace Scripts.Game
{
    public struct CellCloudsDataC
    {
        public bool HaveCloud;
        public CloudWidthTypes CloudWidthType { get; set; }
        public bool IsCenter => CloudWidthType != default;
    }
}