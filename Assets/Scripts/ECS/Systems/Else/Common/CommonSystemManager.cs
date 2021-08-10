using Assets.Scripts.ECS.System.Data.Common;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.System.Common
{
    public sealed class CommonSystemManager : SystemAbstManager
    {
        internal EcsSystems AllSystems { get; private set; }
        internal CommonSystemManager(EcsWorld commonWorld) : base(commonWorld)
        {
            InitSystems
                .Add(new MainCommonSystem());

            AllSystems = new EcsSystems(commonWorld)
                .Add(InitSystems)
                .Add(RunSystems);
        }
    }
}
