using ECS;

namespace Game.Game
{
    public sealed class CellFireE : EntityAbstract
    {
        public ref HaveEffectC Fire => ref Ent.Get<HaveEffectC>();

        public CellFireE(in EcsWorld gameW) : base(gameW)
        {

        }
    }
}