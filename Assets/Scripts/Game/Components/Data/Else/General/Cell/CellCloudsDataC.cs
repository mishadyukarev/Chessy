namespace Scripts.Game
{
    internal struct CellCloudsDataC
    {
        internal bool HaveCloud;
        internal CloudWidthTypes CloudWidthType { get; set; }
        internal bool IsCenter => CloudWidthType != default;
    }
}