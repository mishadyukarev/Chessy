using Leopotam.Ecs;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class MotionCenterUISystem : IEcsRunSystem
    {
        private EcsFilter<MotionsDataUIComponent, MotionsViewUIComponent> _motionsUIFilter = default;
        private float _timer;

        public void Run()
        {
            if (_motionsUIFilter.Get1(0).IsActivatedUI)
            {
                _motionsUIFilter.Get2(0).Text = _motionsUIFilter.Get1(0).AmountMotions.ToString();
                _motionsUIFilter.Get2(0).SetActiveParent(true);

                _timer += Time.deltaTime;

                if (_timer >= 1)
                {
                    _motionsUIFilter.Get2(0).SetActiveParent(false);
                    _motionsUIFilter.Get1(0).IsActivatedUI = false;
                    _timer = 0;
                }
            }
            else
            {
                _motionsUIFilter.Get2(0).SetActiveParent(false);
            }
        }
    }
}