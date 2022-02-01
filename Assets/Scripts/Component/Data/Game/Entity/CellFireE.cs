using ECS;

namespace Game.Game
{
    public sealed class CellFireE : EntityAbstract
    {
        ref HaveFireC HaveFireCRef => ref Ent.Get<HaveFireC>();
        public HaveFireC HaveFireC => Ent.Get<HaveFireC>();

        internal CellFireE(in EcsWorld gameW) : base(gameW)
        {

        }

        public void SetFire(in bool needFire) => HaveFireCRef.Have = needFire;
        public void Disable() => HaveFireCRef.Have = false;
        public void Enable() => HaveFireCRef.Have = true;

        public void SyncRpc(in bool needFire) => HaveFireCRef.Have = needFire;
    }
}