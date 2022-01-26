using ECS;

namespace Game.Game
{
    public sealed class CellE : EntityAbstract
    {
        public ref XyC XyC => ref Ent.Get<XyC>();
        public ref InstanceIDC InstanceIDC => ref Ent.Get<InstanceIDC>();

        public CellE(in EcsWorld gameW, in byte[] xy, in int instanceID) : base(gameW)
        {
            Ent.Add(new XyC(xy))
               .Add(new InstanceIDC(instanceID));
        }
    }
}