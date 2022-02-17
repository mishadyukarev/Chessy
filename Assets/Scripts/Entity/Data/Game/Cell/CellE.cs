using ECS;

namespace Game.Game
{
    public struct CellE
    {
        public XyC XyC;
        public int InstanceIDC;

        public CellE(in byte[] xy, in int instanceID)
        {
            XyC = new XyC(xy);
            InstanceIDC = instanceID;
        }
    }
}