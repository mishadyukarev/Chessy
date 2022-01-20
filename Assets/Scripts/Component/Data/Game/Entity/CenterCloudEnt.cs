using ECS;

namespace Game.Game
{
    public struct CenterCloudEnt
    {
        static Entity _cloud;

        public static ref C CenterCloud<C>() where C : struct, ICenterCloud => ref _cloud.Get<C>();

        public CenterCloudEnt(in EcsWorld gameW)
        {
            _cloud = gameW.NewEntity()
                .Add(new IdxC());
        }
    }

    public interface ICenterCloud { }
}