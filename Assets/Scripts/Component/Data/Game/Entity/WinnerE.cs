using ECS;

namespace Game.Game
{
    public sealed class WinnerE : EntityAbstract
    {
        public ref PlayerTC Winner => ref Ent.Get<PlayerTC>();

        public WinnerE(in EcsWorld world) : base(world)
        {

        }
    }
}