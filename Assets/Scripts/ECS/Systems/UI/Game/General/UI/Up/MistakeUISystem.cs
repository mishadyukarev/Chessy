using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems.Game.General.UI.View
{
    internal sealed class MistakeUISystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<EconomyViewUICom> _economyViewUIFilter = default;

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
            ref var economyViewUICom = ref _economyViewUIFilter.Get1(0);

            if (_isStartedMistake)
            {
                _timer += Time.deltaTime;

                if (_timer >= TIMER_MISTAKE)
                {
                    economyViewUICom.SetMainColor(ResourceTypes.Food, Color.white);
                    economyViewUICom.SetMainColor(ResourceTypes.Wood, Color.white);
                    economyViewUICom.SetMainColor(ResourceTypes.Ore, Color.white);
                    economyViewUICom.SetMainColor(ResourceTypes.Iron, Color.white);
                    economyViewUICom.SetMainColor(ResourceTypes.Gold, Color.white);

                    _isStartedMistake = false;
                    _timer = 0;
                }
            }
        }

        private void MistakeEnvironment(ResourceTypes economyType)
        {
            _economyViewUIFilter.Get1(0).SetMainColor(economyType, Color.red);
            _isStartedMistake = true;
            _timer = 0;
        }
    }
}
