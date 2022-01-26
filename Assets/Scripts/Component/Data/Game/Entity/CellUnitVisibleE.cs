using ECS;

namespace Game.Game
{
    public sealed class CellUnitVisibleE : EntityAbstract
    {
        public ref IsVisibleC VisibleC => ref Ent.Get<IsVisibleC>();

        public CellUnitVisibleE(in EcsWorld gameW) : base(gameW) { }
    }
}