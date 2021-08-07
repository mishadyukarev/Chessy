using Assets.Scripts.ECS.Managers.Event;
using Assets.Scripts.ECS.System.View.Menu;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Manager.View.Menu
{
    public sealed class MenuSystemManager : SystemAbstManager
    {
        internal MenuSystemManager(EcsWorld menuWorld) : base(menuWorld)
        {
            UpdateSystems
                .Add(new MainMenuSystem())
                .Add(Main.Instance.gameObject.AddComponent<PhotonSceneMenuSystem>());
        }
    }
}
