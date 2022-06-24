namespace Chessy.Model
{
    public struct XyCellC
    {
        readonly byte[] _xy;
        public byte X => _xy[0];
        public byte Y => _xy[1];
        public byte[] Xy => (byte[])_xy.Clone();

        internal XyCellC(in byte[] xy) => _xy = xy;
    }
}
