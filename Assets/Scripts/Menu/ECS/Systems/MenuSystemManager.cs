using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Menu
{
    public sealed class MenuSystemManager : SystemAbstManager
    {
        public MenuSystemManager(EcsWorld menuWorld, EcsSystems allMenuSystems) : base(menuWorld, allMenuSystems)
        {
            InitOnlySystems
                .Add(new EventMenuSys());


            UpdateOnlySystems
                .Add(new SyncMenuSys())
                .Add(new ConnectorMenuSys());
        }
    }
}
