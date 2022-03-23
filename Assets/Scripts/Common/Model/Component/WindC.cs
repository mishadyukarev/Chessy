namespace Chessy.Game
{
    public struct WindC
    {
        public DirectTypes Direct;
        public float Speed;

        public float MaxSpeed;
        public float MinSpeed;

        public bool IsMaxSpeed => Speed >= MaxSpeed;
        public bool IsMinSpeed => Speed <= MinSpeed;

        public WindC(in DirectTypes directT, in float speed, in float maxSpeed, in float minSpeed)
        {
            Direct = directT;
            Speed = speed;

            MaxSpeed = maxSpeed;
            MinSpeed = minSpeed;
        }
    }
}