using ECS;

namespace Game.Game
{
    public sealed class ActiveSnowyAroundME : EntityAbstract
    {
        public ref IdxC Where => ref Ent.Get<IdxC>();

        public ActiveSnowyAroundME(in EcsWorld world) : base(world)
        {
        }
    }
}