using static Assets.Scripts.Abstractions.ValuesConst;

internal struct XyCellComponent
{
    private int[] _xyCell;

    internal int[] XyCell
    {
        get => _xyCell;
        set => _xyCell = value;
    }

    internal int[] XyCellCopy
    {
        get
        {
            int[] xyCell = new int[XY_FOR_ARRAY];

            xyCell[X] = _xyCell[X];
            xyCell[Y] = _xyCell[Y];

            return xyCell;
        }

        set
        {
            _xyCell[X] = value[X];
            _xyCell[Y] = value[Y];
        }
    }

    internal void StartFill()
    {

    }
}

