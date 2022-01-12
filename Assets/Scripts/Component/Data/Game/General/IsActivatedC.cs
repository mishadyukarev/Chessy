namespace Game.Game
{
    public struct IsActivatedC
    {
        public bool IsActivated;

        internal IsActivatedC(in bool isActivated) => IsActivated = isActivated;

        public void Toggle() => IsActivated = !IsActivated;
    }
}
