internal struct XyCellComponent
{
    private byte[] _xyCell;

    internal byte[] XyCell
    {
        get => (byte[])_xyCell.Clone();
        set => _xyCell = (byte[])value.Clone();
    }

    internal XyCellComponent(byte[] xy) => _xyCell = xy;
}

