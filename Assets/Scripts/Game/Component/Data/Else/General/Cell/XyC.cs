namespace Chessy.Game
{
    public struct XyC
    {
        private byte[] _xy;
        public readonly byte Idx;

        public byte[] Xy
        {
            get => (byte[])_xy.Clone();
            set => _xy = (byte[])value.Clone();
        }

        public XyC(byte idx, byte[] xy)
        {
            Idx = idx;
            _xy = xy;
        }
    }
}
