using ECS;

namespace Game.Game
{
    public sealed class IceWallME : EntityAbstract
    {
        public ref IdxC IdxC => ref Ent.Get<IdxC>();

        public IceWallME(in EcsWorld gameW) : base(gameW)
        {

        }
    }
}