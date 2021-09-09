using Assets.Scripts.ECS.System.Data.Common;
using Assets.Scripts.ECS.Systems.Else.Common;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.System.Common
{
    public sealed class ComSysManager : SystemAbstManager
    {
        internal ComSysManager(EcsWorld comWorld, EcsSystems allComSystems) : base(comWorld, allComSystems)
        {
            InitOnlySystems
                .Add(new MainComSys());

            RunOnlySystems
                .Add(new SyncSoundComSys());


            //allCommSystems
            //    .Add(InitOnlySystems)
            //    .Add(RunOnlySystems)
            //    .Add(InitRunSystems);
        }
    }
}
