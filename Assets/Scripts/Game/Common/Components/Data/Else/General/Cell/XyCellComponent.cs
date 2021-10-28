namespace Scripts.Game
{
    public struct XyCellComponent
    {
        private byte[] _xyCell;

        public byte[] XyCell
        {
            get => (byte[])_xyCell.Clone();
            set => _xyCell = (byte[])value.Clone();
        }

        public XyCellComponent(byte[] xy) => _xyCell = xy;
    }
}
