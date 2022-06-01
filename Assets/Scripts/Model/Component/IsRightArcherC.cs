namespace Chessy.Game
{
    public struct IsRightArcherC
    {
        public bool IsRight { get; internal set; }

        internal void ToggleSide() => IsRight = !IsRight;
    }
}