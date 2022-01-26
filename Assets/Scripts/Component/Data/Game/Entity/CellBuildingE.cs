using ECS;

namespace Game.Game
{
    public sealed class CellBuildingE : EntityAbstract
    {
        public ref BuildingTC BuildTC => ref Ent.Get<BuildingTC>();
        public ref PlayerTC PlayerTC => ref Ent.Get<PlayerTC>();

        public CellBuildingE(in EcsWorld world) : base(world)
        {
        }
    }
}