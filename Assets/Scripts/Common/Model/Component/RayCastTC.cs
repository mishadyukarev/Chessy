namespace Chessy.Game
{
    public struct RaycastTC
    {
        public RaycastTypes Raycast;

        public bool Is(in RaycastTypes raycast) => Raycast == raycast;

        public void Reset() => Raycast = default;
    }
}