using static Assets.Scripts.Abstractions.ValuesConst;

internal struct XyCellComponent
{
    private int[] _xyCell;

    internal int[] XyCell
    {
        get => (int[])_xyCell.Clone();
        set => _xyCell = (int[])value.Clone();
    }

    internal void StartFill() => _xyCell = new int[XY_FOR_ARRAY];
}

