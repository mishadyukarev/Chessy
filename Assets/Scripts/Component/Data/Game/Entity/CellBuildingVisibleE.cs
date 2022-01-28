using ECS;

namespace Game.Game
{
    public sealed class CellBuildingVisibleE : EntityAbstract
    {
        public ref IsVisibleC IsVisibleC => ref Ent.Get<IsVisibleC>();

        public CellBuildingVisibleE(in EcsWorld world) : base(world) { }
    }
}