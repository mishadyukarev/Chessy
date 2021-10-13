using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Menu
{
    public sealed class MenuSystemManager : SystemAbstManager
    {
        public LaunchLikeGameAndShopSys LaunchLikeGameSys { get; private set; }

        public MenuSystemManager(EcsWorld menuWorld, EcsSystems allMenuSystems) : base(menuWorld, allMenuSystems)
        {
            LaunchLikeGameSys = new LaunchLikeGameAndShopSys();


            InitOnlySystems
                .Add(new EventMenuSys())
                .Add(LaunchLikeGameSys);


            UpdateOnlySystems
                .Add(new SyncMenuSys())
                .Add(new ConnectorMenuSys());
        }
    }
}
