namespace Scripts.Game
{
    public struct XyCellComponent
    {
        private byte[] _xyCell;
        public readonly byte Idx;

        public byte[] XyCell
        {
            get => (byte[])_xyCell.Clone();
            set => _xyCell = (byte[])value.Clone();
        }

        public XyCellComponent(byte idx, byte[] xy)
        {
            Idx = idx;
            _xyCell = xy;
        }
    }
}
