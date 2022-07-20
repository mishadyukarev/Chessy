namespace Chessy.Model
{
    public sealed class CloudC
    {
        internal bool IsCenter;

        public bool IsCenterP => IsCenter;

        internal void Dispose()
        {
            IsCenter = default;
        }
        internal void Clone(in CloudC newCloudC)
        {
            IsCenter = newCloudC.IsCenter;
        }
    }
}