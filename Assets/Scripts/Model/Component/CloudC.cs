namespace Chessy.Model
{
    public struct CloudC
    {
        public bool HaveCloud { get; internal set; }
        public bool IsCenter { get; internal set; }

        internal void SetCloud(in bool isCenter)
        {
            HaveCloud = true;
            IsCenter = isCenter;
        }
        internal void Dispose()
        {
            HaveCloud = default;
            IsCenter = default;
        }
    }
}