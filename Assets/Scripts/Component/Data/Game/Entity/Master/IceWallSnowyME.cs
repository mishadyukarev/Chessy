using ECS;

namespace Game.Game
{
    public sealed class IceWallSnowyME : EntityAbstract
    {
        public ref IdxC IdxC => ref Ent.Get<IdxC>();

        public IceWallSnowyME(in EcsWorld gameW) : base(gameW)
        {

        }
    }
}