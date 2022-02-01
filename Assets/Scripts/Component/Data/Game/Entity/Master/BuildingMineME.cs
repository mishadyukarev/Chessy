using ECS;

namespace Game.Game
{
    public class BuildingMineME : EntityAbstract
    {
        public ref IdxC WhereBuildMine => ref Ent.Get<IdxC>();

        public BuildingMineME(in EcsWorld world) : base(world)
        {
        }
    }
}