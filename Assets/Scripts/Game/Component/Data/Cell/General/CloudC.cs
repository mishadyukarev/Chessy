namespace Chessy.Game
{
    public struct CloudC
    {
        public bool HaveCloud;
        public CloudWidthTypes CloudWidthType { get; set; }
        public bool IsCenter => CloudWidthType != default;
    }
}