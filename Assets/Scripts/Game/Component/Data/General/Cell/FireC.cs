namespace Game.Game
{
    public struct FireC : IElseCell
    {
        public bool Have { get; private set; }

        public bool Disable() => Have = default;
        public bool Enable() => Have = true;

        public void Sync(bool have) => Have = have;
    }
}
