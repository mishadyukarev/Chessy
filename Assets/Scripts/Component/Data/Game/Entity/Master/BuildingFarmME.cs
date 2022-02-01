using ECS;

namespace Game.Game
{
    public sealed class BuildingFarmME : EntityAbstract
    {
        public ref IdxC IdxC => ref Ent.Get<IdxC>();

        public BuildingFarmME(in EcsWorld world) : base(world)
        {
        }
    }
}