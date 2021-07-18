using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

internal struct XyCellComponent
{
    private int[] _xyCell;

    internal int[] XyCell => (int[])_xyCell.Clone();

    internal void StartFill() => _xyCell = new int[XY_FOR_ARRAY];

    internal void SetXyCell(int[] xy) => _xyCell = (int[])xy.Clone();
}

