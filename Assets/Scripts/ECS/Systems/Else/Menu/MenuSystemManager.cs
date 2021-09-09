using Assets.Scripts.ECS.Managers.Event;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Manager.View.Menu
{
    public sealed class MenuSystemManager : SystemAbstManager
    {
        internal PhotonSceneMenuSystem PhotonSceneMenuSystem { get; private set; }

        internal MenuSystemManager(EcsWorld menuWorld, EcsSystems allMenuSystems) : base(menuWorld, allMenuSystems)
        {
            PhotonSceneMenuSystem = new PhotonSceneMenuSystem();

            InitOnlySystems
                .Add(PhotonSceneMenuSystem);
        }
    }
}
