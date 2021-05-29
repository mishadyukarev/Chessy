internal struct CellComponent
{
    private int[] _xy;
    internal int X => _xy[0];
    internal int Y => _xy[1];

    internal CellComponent(params int[] xy)
    {
        _xy = xy;
    }
}
