namespace Chessy.Model
{
    public struct CloudC
    {
        internal bool IsCenter;

        public bool IsCenterP => IsCenter;

        internal void Dispose()
        {
            IsCenter = default;
        }
    }
}