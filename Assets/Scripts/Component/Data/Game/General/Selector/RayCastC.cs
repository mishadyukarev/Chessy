namespace Game.Game
{
    public struct RayCastC : IClickerObjectE
    {
        public RaycastTypes Raycast { private get; set; }

        public bool Is(in RaycastTypes raycast) => Raycast == raycast;

        public void Reset() => Raycast = default; 
    }
}