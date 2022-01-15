namespace Game.Game
{
    public struct IsActiveC : ICell
    {
        public bool IsActive;

        internal IsActiveC(in bool isActivated) => IsActive = isActivated;

        public void Toggle() => IsActive = !IsActive;
    }
}
