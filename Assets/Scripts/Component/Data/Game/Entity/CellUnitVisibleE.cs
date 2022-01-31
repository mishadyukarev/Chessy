using ECS;

namespace Game.Game
{
    public sealed class CellUnitVisibleE : EntityAbstract
    {
        public ref IsVisibleC IsVisibleC => ref Ent.Get<IsVisibleC>();

        public CellUnitVisibleE(in EcsWorld gameW) : base(gameW) { }
    }
}