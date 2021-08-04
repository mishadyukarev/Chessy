using Assets.Scripts.ECS.Entities.Game.General.UI.Containers;
using Assets.Scripts.ECS.Entities.Game.General.UI.Data.Containers;
using Assets.Scripts.Workers.Info;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.System.Data.Game.General.UI
{
    public sealed class SysDataGameGeneralUIManager : SystemAbstManager
    {
        internal SysDataGameGeneralUIManager(EcsWorld gameWorld) : base(gameWorld)
        {
            new MistakeDataUIContainer(gameWorld);
            new ResourcesUIDataContainer(new ResourcesDataUIContainer(gameWorld));
        }
    }
}
