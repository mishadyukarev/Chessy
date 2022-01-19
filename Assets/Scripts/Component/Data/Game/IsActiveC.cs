namespace Game.Game
{
    public struct IsActiveC
    {
        public bool IsActive;

        internal IsActiveC(in bool isActivated) => IsActive = isActivated;

        public void Toggle() => IsActive = !IsActive;
    }
}
