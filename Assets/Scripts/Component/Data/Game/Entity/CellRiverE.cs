using ECS;

namespace Game.Game
{
    public sealed class CellRiverE : EntityAbstract
    {
        public ref RiverTC RiverTC => ref Ent.Get<RiverTC>();

        public CellRiverE(in EcsWorld gameW) : base(gameW)
        {

        }
    }
}