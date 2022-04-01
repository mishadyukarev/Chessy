namespace Chessy.Game
{
    public struct RaycastTC
    {
        public RaycastTypes RaycastT { get; internal set; }

        public bool Is(in RaycastTypes raycast) => RaycastT == raycast;

        public void Reset() => RaycastT = default;
    }
}