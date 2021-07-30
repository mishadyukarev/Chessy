using Assets.Scripts.Workers.Game.Else.Data;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Game.General.UI.View.Down
{
    internal sealed class MiddleMistakeUISystem : IEcsInitSystem, IEcsRunSystem
    {
        public void Init()
        {
            MistakeEconomyEventDataWorker.AddListenerEconomyMistake(ResourceTypes.Food, ExecuteMistakeText);
            MistakeEconomyEventDataWorker.AddListenerEconomyMistake(ResourceTypes.Wood, ExecuteMistakeText);
            MistakeEconomyEventDataWorker.AddListenerEconomyMistake(ResourceTypes.Ore, ExecuteMistakeText);
            MistakeEconomyEventDataWorker.AddListenerEconomyMistake(ResourceTypes.Iron, ExecuteMistakeText);
            MistakeEconomyEventDataWorker.AddListenerEconomyMistake(ResourceTypes.Gold, ExecuteMistakeText);

        }

        public void Run()
        {

        }

        private void ExecuteMistakeText()
        {
            //MiddleViewUIWorker.
        }
    }
}
