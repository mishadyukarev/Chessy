using static Assets.Scripts.Abstractions.ValuesConst;

internal struct CellComponent
{
    private int[] _xy;
    internal int[] XyCopy
    {
        get
        {
            var xy = new int[XY_FOR_ARRAY];

            xy[X] = _xy[X];
            xy[Y] = _xy[Y];

            return xy;
        }
    }

    internal void StartFill(params int[] xy) => _xy = xy;
}
