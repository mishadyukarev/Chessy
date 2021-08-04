using Assets.Scripts.ECS.System.View.Common;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Manager.View.Common
{


    public sealed class CommonElseViewSysManager : SystemAbstManager
    {
        internal CommonElseViewSysManager(EcsWorld commWorld) : base(commWorld)
        {
            UpdateSystems
                .Add(new MainCommonViewSys());
        }
    }
}
