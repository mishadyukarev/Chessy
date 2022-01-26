using ECS;

namespace Game.Game
{
    public sealed class CellTrailPlayerE : EntityAbstract
    {
        public ref IsVisibleC IsVisibleC => ref Ent.Get<IsVisibleC>();

        public CellTrailPlayerE(in EcsWorld world) : base(world)
        {
        }
    }
}