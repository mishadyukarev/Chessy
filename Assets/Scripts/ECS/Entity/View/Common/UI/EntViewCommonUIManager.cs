using Assets.Scripts.Workers.Common;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Entity.View.Common.UI
{
    public sealed class EntViewCommonUIManager
    {
        internal ViewCommonContainerUICanvas ViewCommonContainerUICanvas { get; private set; }

        internal EntViewCommonUIManager(EcsWorld commonWorld, EntDataCommonElseManager entDataCommonElseManager)
        {
            ViewCommonContainerUICanvas =
                new ViewCommonContainerUICanvas(commonWorld, entDataCommonElseManager.ResourcesEnt_ResourcesCommonCom.PrefabConfig.Canvas);
        }
    }
}
