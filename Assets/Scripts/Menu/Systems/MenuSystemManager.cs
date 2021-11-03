using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Menu
{
    public sealed class MenuSystemManager : SystemAbstManager
    {
        public EcsSystems LaunchLikeGameSys { get; private set; }

        public MenuSystemManager(EcsWorld menuWorld, EcsSystems allMenuSystems) : base(menuWorld, allMenuSystems)
        {
            LaunchLikeGameSys = new EcsSystems(menuWorld)
                .Add(new LaunchLikeGameAndShopSys());


            InitOnlySystems
                .Add(new EventSys())
                .Add(LaunchLikeGameSys);


            UpdateOnlySystems
                .Add(new SyncSys())
                .Add(new ConnectorMenuSys());
        }
    }
}
