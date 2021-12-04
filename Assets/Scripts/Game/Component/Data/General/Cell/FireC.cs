namespace Game.Game
{
    public struct FireC : IFireCell
    {
        public bool Have { get; private set; }

        public void Set(bool have) => Have = have;
        public void Disable() => Have = false;
        public void Enable() => Have = true;

        public void Sync(bool have) => Have = have;
    }
}
