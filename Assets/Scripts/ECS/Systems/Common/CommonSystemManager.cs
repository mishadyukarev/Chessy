using Assets.Scripts.ECS.System.Data.Common;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.System.Common
{
    public sealed class CommonSystemManager : SystemAbstManager
    {
        internal CommonSystemManager(EcsWorld commonWorld) : base(commonWorld)
        {
            UpdateSystems
                .Add(new MainCommonSystem());
        }
    }
}
