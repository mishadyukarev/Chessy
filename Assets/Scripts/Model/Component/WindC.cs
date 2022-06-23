namespace Chessy.Model
{
    public struct WindC
    {
        public DirectTypes DirectT { get; internal set; }
        public float Speed { get; internal set; }

        internal WindC(in DirectTypes directT, in float speed)
        {
            DirectT = directT;
            Speed = speed;
        }
    }
}