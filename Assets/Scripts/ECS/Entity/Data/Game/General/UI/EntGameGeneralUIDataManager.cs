using Assets.Scripts.ECS.Entities.Game.General.UI.Containers;
using Assets.Scripts.ECS.Entities.Game.General.UI.Data.Containers;
using Assets.Scripts.Workers.Info;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Entities.Game.General
{
    public sealed class EntGameGeneralUIDataManager
    {
        private MistakeDataUIContainer _mistakeDataUIContainer;
        private ResourcesDataUIContainer _resourcesDataUIContainer;

        internal EntGameGeneralUIDataManager(EcsWorld gameWorld)
        {
            _mistakeDataUIContainer = new MistakeDataUIContainer(gameWorld);

            _resourcesDataUIContainer = new ResourcesDataUIContainer(gameWorld);
            new ResourcesUIDataContainer(_resourcesDataUIContainer);
        }
    }
}
