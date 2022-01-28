namespace Game.Game
{
    public struct RayCastTC
    {
        public RaycastTypes Raycast;

        public bool Is(in RaycastTypes raycast) => Raycast == raycast;

        public void Reset() => Raycast = default;
    }
}