using ECS;

namespace Chessy.Game
{
    public abstract class EntityAbstract
    {
        protected readonly Entity Ent;

        public EntityAbstract(in EcsWorld world)
        {
            Ent = world.NewEntity();
        }
    }
}