namespace Chessy.Game
{
    public struct XyCellC
    {
        readonly byte[] _xy;

        public byte[] Xy => (byte[])_xy.Clone();
        public byte X => _xy[0];
        public byte Y => _xy[1];

        public XyCellC(in byte[] xy) => _xy = xy;
    }
}
