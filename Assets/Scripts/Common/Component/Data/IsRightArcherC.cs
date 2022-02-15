namespace Game.Game
{
    public struct IsRightArcherC
    {
        public bool IsRight;

        public void ToggleSide() => IsRight = !IsRight;
    }
}