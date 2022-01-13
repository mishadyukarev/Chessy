using ECS;

namespace Game.Game
{
    public struct CloudEnt
    {
        static Entity _cloud;

        public static ref C Cloud<C>() where C : struct, ICloud => ref _cloud.Get<C>();

        public CloudEnt(in EcsWorld gameW)
        {
            _cloud = gameW.NewEntity()
                .Add(new IdxC());
        }
    }

    public interface ICloud { }
}