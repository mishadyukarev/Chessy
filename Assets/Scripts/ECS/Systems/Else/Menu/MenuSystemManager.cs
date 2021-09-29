using Assets.Scripts.ECS.Managers.Event;
using Assets.Scripts.ECS.Systems.Else.Common;
using Assets.Scripts.ECS.Systems.Else.Menu;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Manager.View.Menu
{
    public sealed class MenuSystemManager : SystemAbstManager
    {
        internal MenuSystemManager(EcsWorld menuWorld, EcsSystems allMenuSystems) : base(menuWorld, allMenuSystems)
        {
            InitOnlySystems
                .Add(new EventMenuSys());


            UpdateOnlySystems
                .Add(new SyncMenuSys())
                .Add(new ConnectorMenuSys());
        }
    }
}
