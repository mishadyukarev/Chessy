using ECS;

namespace Chessy.Game
{
    public sealed class BuildingUpgradesE : EntityAbstract
    {
        public ref HaveC HaveUpgrade => ref Ent.Get<HaveC>();

        public BuildingUpgradesE(in EcsWorld gameW) : base(gameW) { }
    }
}