namespace Game.Game
{
    public struct XyC : ICell
    {
        private byte[] _xy;

        public byte[] Xy
        {
            get => (byte[])_xy.Clone();
            set => _xy = (byte[])value.Clone();
        }

        public XyC(byte[] xy)
        {
            _xy = xy;
        }
    }
}
