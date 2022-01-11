namespace Game.Game
{
    public struct XyC : EntityCellPool.ICell
    {
        readonly byte[] _xy;

        public byte[] Xy => (byte[])_xy.Clone();

        public XyC(in byte[] xy)
        {
            _xy = xy;
        }
    }
}
