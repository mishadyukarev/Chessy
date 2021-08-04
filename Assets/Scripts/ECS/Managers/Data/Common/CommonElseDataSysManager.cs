using Assets.Scripts.ECS.System.Common.RunUpdate;
using Assets.Scripts.ECS.System.Data.Common;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.System.Common
{
    public sealed class CommonElseDataSysManager : SystemAbstManager
    {
        internal CommonElseDataSysManager(EcsWorld commonWorld) : base(commonWorld)
        {
            UpdateSystems
                .Add(new MainDataCommSys())

                .Add(new SyncComDataSystem());
        }
    }
}
