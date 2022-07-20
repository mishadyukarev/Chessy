namespace Chessy.Model
{
    public sealed class ZonesInfoC
    {
        public bool IsActiveFriend { get; internal set; }
        public bool IsActiveEnvironment { get; internal set; }

        internal void Dispose()
        {
            IsActiveFriend = default;
            IsActiveEnvironment = default;
        }
    }
}