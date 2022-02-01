using ECS;

namespace Game.Game
{
    public sealed class ActiveIceWallME : EntityAbstract
    {
        public ref IdxC WhereActiveIceWall => ref Ent.Get<IdxC>();

        public ActiveIceWallME(in EcsWorld world) : base(world)
        {
        }
    }
}