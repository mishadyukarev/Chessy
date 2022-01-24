using ECS;

namespace Game.Game
{
    public abstract class EntityAbtract
    {
        protected readonly Entity Ent;

        public EntityAbtract(in EcsWorld world)
        {
            Ent = world.NewEntity();
        }
    }
}