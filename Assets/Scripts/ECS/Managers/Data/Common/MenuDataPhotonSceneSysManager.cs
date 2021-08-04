using Assets.Scripts.ECS.Systems.Data.Common;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Managers.Data.Common
{
    public class MenuDataPhotonSceneSysManager : SystemAbstManager
    {
        internal MenuDataPhotonSceneSysManager(EcsWorld commonWorld) : base(commonWorld)
        {
            UpdateSystems
                .Add(new SysDataMenuPhotonSceneMain());
        }
    }
}
