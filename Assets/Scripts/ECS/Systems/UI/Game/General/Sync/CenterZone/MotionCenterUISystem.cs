using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Leopotam.Ecs;
using UnityEngine;

internal sealed class MotionCenterUISystem : IEcsRunSystem
{
    private EcsFilter<MotionsDataUIComponent, MotionsViewUIComponent> _motionsUIFilter = default;
    private float _timer;

    public void Run()
    {
        if (_motionsUIFilter.Get1(0).IsActivatedUI)
        {
            _motionsUIFilter.Get2(0).Text = "Motion: " + _motionsUIFilter.Get1(0).AmountMotions;
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
