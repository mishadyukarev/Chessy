using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.Workers.Game.UI.Vis.Up;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems.Game.General.UI.View
{
    internal sealed class MistakeUISystem : IEcsInitSystem, IEcsRunSystem
    {

        private const float TIMER_MISTAKE = 1;

        private float _timer;
        private bool _isStartedMistake;

        public void Init()
        {
            MainGameSystem.MistakeCom.AddListenerEconomyMistake(ResourceTypes.Food, delegate { MistakeEnvironment(ResourceTypes.Food); });
            MainGameSystem.MistakeCom.AddListenerEconomyMistake(ResourceTypes.Wood, delegate { MistakeEnvironment(ResourceTypes.Wood); });
            MainGameSystem.MistakeCom.AddListenerEconomyMistake(ResourceTypes.Ore, delegate { MistakeEnvironment(ResourceTypes.Ore); });
            MainGameSystem.MistakeCom.AddListenerEconomyMistake(ResourceTypes.Iron, delegate { MistakeEnvironment(ResourceTypes.Iron); });
            MainGameSystem.MistakeCom.AddListenerEconomyMistake(ResourceTypes.Gold, delegate { MistakeEnvironment(ResourceTypes.Gold); });
        }

        public void Run()
        {
            if (_isStartedMistake)
            {
                _timer += Time.deltaTime;

                if (_timer >= TIMER_MISTAKE)
                {
                    ResourcesViewUIWorker.SetMainColor(ResourceTypes.Food, Color.white);
                    ResourcesViewUIWorker.SetMainColor(ResourceTypes.Wood, Color.white);
                    ResourcesViewUIWorker.SetMainColor(ResourceTypes.Ore, Color.white);
                    ResourcesViewUIWorker.SetMainColor(ResourceTypes.Iron, Color.white);
                    ResourcesViewUIWorker.SetMainColor(ResourceTypes.Gold, Color.white);

                    _isStartedMistake = false;
                    _timer = 0;
                }
            }
        }

        private void MistakeEnvironment(ResourceTypes economyType)
        {
            ResourcesViewUIWorker.SetMainColor(economyType, Color.red);
            _isStartedMistake = true;
            _timer = 0;
        }
    }
}
