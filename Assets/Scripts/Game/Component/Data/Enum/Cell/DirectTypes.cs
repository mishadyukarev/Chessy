namespace Game.Game
{
    public enum DirectTypes : byte
    {
        None,
        Start = None,

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