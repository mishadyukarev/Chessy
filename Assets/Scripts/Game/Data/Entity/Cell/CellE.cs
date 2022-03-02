namespace Chessy.Game
{
    public readonly struct CellE
    {
        public readonly IdxCellC IdxC;
        public readonly XyC XyC;
        public readonly int InstanceIDC;

        public CellE(in byte idx, in byte[] xy, in int instanceID)
        {
            IdxC = new IdxCellC(idx);
            XyC = new XyC(xy);
            InstanceIDC = instanceID;
        }
    }
}