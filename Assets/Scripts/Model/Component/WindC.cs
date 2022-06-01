namespace Chessy.Game
{
    public struct WindC
    {
        public DirectTypes DirectT { get; internal set; }
        public float Speed { get; internal set; }

        public float MaxSpeed { get; internal set; }
        public float MinSpeed { get; internal set; }

        public bool IsMaxSpeed => Speed >= MaxSpeed;
        public bool IsMinSpeed => Speed <= MinSpeed;

        internal WindC(in DirectTypes directT, in float speed, in float maxSpeed, in float minSpeed)
        {
            DirectT = directT;
            Speed = speed;

            MaxSpeed = maxSpeed;
            MinSpeed = minSpeed;
        }
    }
}