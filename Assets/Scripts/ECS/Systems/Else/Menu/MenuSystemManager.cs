using Assets.Scripts.ECS.Managers.Event;
using Assets.Scripts.ECS.System.View.Menu;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Manager.View.Menu
{
    public sealed class MenuSystemManager : SystemAbstManager
    {
        internal MenuSystemManager(EcsWorld menuWorld, EcsSystems allMenuSystems) : base(menuWorld)
        {
            InitOnlySystems
                .Add(new MainMenuSystem())
                .Add(Main.Instance.gameObject.AddComponent<PhotonSceneMenuSystem>());

            allMenuSystems
                .Add(InitOnlySystems)
                .Add(RunOnlySystems)
                .Add(InitRunSystems);
        }
    }
}
