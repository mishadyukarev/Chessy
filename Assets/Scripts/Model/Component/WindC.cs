namespace Chessy.Model
{
    public struct WindC
    {
        public DirectTypes DirectT { get; internal set; }
        public byte Speed { get; internal set; }

        internal WindC(in DirectTypes directT, in byte speed)
        {
            DirectT = directT;
            Speed = speed;
        }
    }
}