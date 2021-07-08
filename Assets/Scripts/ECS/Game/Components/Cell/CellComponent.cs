using static Assets.Scripts.Abstractions.ValuesConst;

internal struct CellComponent
{
    private int[] _xy;
    internal int[] XyClone => (int[])_xy.Clone();

    internal void StartFill(params int[] xy) => _xy = xy;
}
