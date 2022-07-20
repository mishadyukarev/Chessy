namespace Chessy.Model
{
    public sealed class WindC
    {
        internal DirectTypes DirectT;
        internal byte Speed;

        public DirectTypes DirectType => DirectT;
        public byte SpeedP => Speed;

        internal void Set(in DirectTypes directT, in byte speed)
        {
            DirectT = directT;
            Speed = speed;
        }
        internal void Dispose()
        {
            DirectT = default;
            Speed = default;
        }
    }
}