namespace Chessy.Model
{
    public struct CloudC
    {
        public bool IsCenter { get; internal set; }

        internal void Dispose()
        {
            IsCenter = default;
        }
    }
}