using Assets.Scripts.ECS.Entities.Game.General.UI.Containers;
using Assets.Scripts.ECS.Entities.Game.General.UI.Data.Containers;
using Assets.Scripts.Workers.Info;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Entities.Game.General
{
    public sealed class EntDataGameGeneralUIManager
    {
        private MistakeDataUIContainer _mistakeDataUIContainer;
        private ResourcesDataUIContainer _resourcesDataUIContainer;

        internal EntDataGameGeneralUIManager(EcsWorld gameWorld)
        {
            _mistakeDataUIContainer = new MistakeDataUIContainer(gameWorld);

            _resourcesDataUIContainer = new ResourcesDataUIContainer(gameWorld);
            new ResourcesUIDataContainer(_resourcesDataUIContainer);
        }
    }
}
