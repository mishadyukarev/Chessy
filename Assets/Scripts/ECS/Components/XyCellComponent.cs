using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

internal struct XyCellComponent
{
    private int[] _xyCell;

    internal int[] XyCell
    {
        get => (int[])_xyCell.Clone();
        set => _xyCell = (int[])value.Clone();
    }

    internal XyCellComponent(int[] xy) => _xyCell = xy;
}

