using Assets.Scripts.ECS.Managers.Event;
using Assets.Scripts.ECS.Systems.Else.Common;
using Assets.Scripts.ECS.Systems.Else.Menu;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Manager.View.Menu
{
    public sealed class MenuSystemManager : SystemAbstManager
    {
        internal static EventMenuSys PhotSceneMenuSys { get; private set; }
        internal static ConnectToMasterMenuSys ConnectToMasterMenuSys { get; private set; }
        internal static ConnUsingSettingsMenuSys ConnUsingSettingsMenuSys { get; private set; }

        internal MenuSystemManager(EcsWorld menuWorld, EcsSystems allMenuSystems) : base(menuWorld, allMenuSystems)
        {
            PhotSceneMenuSys = new EventMenuSys();
            ConnectToMasterMenuSys = new ConnectToMasterMenuSys();
            ConnUsingSettingsMenuSys = new ConnUsingSettingsMenuSys();

            InitOnlySystems
                .Add(PhotSceneMenuSys);


            RunOnlySystems
                .Add(new SyncMenuSys());


            allMenuSystems
                .Add(ConnectToMasterMenuSys)
                .Add(ConnUsingSettingsMenuSys);
        }
    }
}
