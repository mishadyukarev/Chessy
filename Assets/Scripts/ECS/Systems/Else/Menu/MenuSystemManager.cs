using Assets.Scripts.ECS.Managers.Event;
using Assets.Scripts.ECS.Systems.Else.Common;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Manager.View.Menu
{
    public sealed class MenuSystemManager : SystemAbstManager
    {
        internal static EvenPhotSceneMenuSys PhotSceneMenuSys { get; private set; }

        internal MenuSystemManager(EcsWorld menuWorld, EcsSystems allMenuSystems) : base(menuWorld, allMenuSystems)
        {
            PhotSceneMenuSys = new EvenPhotSceneMenuSys();

            InitOnlySystems
                .Add(PhotSceneMenuSys);

            RunOnlySystems
                .Add(new SyncMenuSys());
        }
    }
}
