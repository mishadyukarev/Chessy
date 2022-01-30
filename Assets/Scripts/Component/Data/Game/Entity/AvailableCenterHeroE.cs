using ECS;

namespace Game.Game
{
    public sealed class AvailableCenterHeroE : EntityAbstract
    {
        public ref HaveC HaveCenterHero => ref Ent.Get<HaveC>();

        public AvailableCenterHeroE(in bool haveCenterHero, in EcsWorld world) : base(world)
        {
            Ent.Add(new HaveC(haveCenterHero));
        }
    }
}