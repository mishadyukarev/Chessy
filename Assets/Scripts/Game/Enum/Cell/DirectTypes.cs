namespace Game.Game
{
    public enum DirectTypes : byte
    {
        Start,
        None = Start,

        Up,
        First = Up,

        UpRight,
        Right,
        DownRight,
        Down,
        DownLeft,
        Left,
        UpLeft,

        End
    }
}