using Assets.Scripts.ECS.Managers.Event;
using Assets.Scripts.ECS.System.View.Menu;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Manager.View.Menu
{
    public sealed class MenuSystemManager : SystemAbstManager
    {
        internal EcsSystems AllSystems { get; private set; }
        internal MenuSystemManager(EcsWorld menuWorld) : base(menuWorld)
        {
            InitSystems
                .Add(new MainMenuSystem())
                .Add(Main.Instance.gameObject.AddComponent<PhotonSceneMenuSystem>());

            AllSystems = new EcsSystems(menuWorld)
                .Add(InitSystems)
                .Add(RunSystems);
        }
    }
}
