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
        internal CloudC Clone()
        {
            var cloudC = new CloudC();
            cloudC.IsCenter = IsCenter;
            return cloudC;
        }
        internal void Copy(in CloudC newCloudC)
        {
            IsCenter = newCloudC.IsCenter;
        }
    }
}