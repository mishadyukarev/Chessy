using ECS;

namespace Game.Game
{
    public sealed class BuildingUpgradesE : EntityAbstract
    {
        public ref HaveC HaveUpgrade => ref Ent.Get<HaveC>();

        public BuildingUpgradesE(in EcsWorld gameW) : base(gameW) { }
    }
}