using ECS;

namespace Game.Game
{
    public sealed class HaveBuildE : EntityAbstract
    {
        public ref HaveC HaveBuilding => ref Ent.Get<HaveC>();

        public HaveBuildE(in EcsWorld gameW) : base(gameW) { }
    }
}
