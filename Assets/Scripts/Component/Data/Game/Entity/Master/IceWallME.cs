using ECS;

namespace Game.Game
{
    public struct IceWallME
    {
        static Entity _ent;

        public static ref IdxC IdxC => ref _ent.Get<IdxC>();

        public IceWallME(in EcsWorld gameW)
        {
            _ent = gameW.NewEntity()
                .Add(new IdxC());
        }
    }
}