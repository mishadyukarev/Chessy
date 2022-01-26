using ECS;

namespace Game.Game
{
    public sealed class PreviousVisionIdxE : EntityAbstract
    {
        public ref IdxC IdxC => ref Ent.Get<IdxC>();

        public PreviousVisionIdxE(in EcsWorld world) : base(world)
        {

        }
    }
}