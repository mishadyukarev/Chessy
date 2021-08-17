using Assets.Scripts.ECS.System.Data.Common;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.System.Common
{
    public sealed class CommonSystemManager : SystemAbstManager
    {
        internal CommonSystemManager(EcsWorld commonWorld, EcsSystems allCommSystems) : base(commonWorld)
        {
            InitOnlySystems
                .Add(new MainCommonSystem());

            allCommSystems
                .Add(InitOnlySystems)
                .Add(RunOnlySystems)
                .Add(InitRunSystems);
        }
    }
}
