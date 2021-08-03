using Assets.Scripts.ECS.System.Common.RunUpdate;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.System.Common
{
    public sealed class SysDataCommonManager : SystemAbstManager
    {
        internal SysDataCommonManager(EcsWorld commonWorld) : base(commonWorld)
        {
            RunUpdateSystems.Add(new SyncComDataSystem());
        }

        internal override void Init()
        {
            base.Init();
        }

        internal override void RunUpdate()
        {
            base.RunUpdate();


        }
    }
}
