using Leopotam.Ecs;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class MotionCenterUISystem : IEcsRunSystem
    {
        private float _timer;

        public void Run()
        {
            if (MotionsDataUIC.IsActivated)
            {
                MotionsViewUIC.Text = MotionsDataUIC.AmountMotions.ToString();
                MotionsViewUIC.SetActiveParent(true);

                _timer += Time.deltaTime;

                if (_timer >= 1)
                {
                    MotionsViewUIC.SetActiveParent(false);
                    MotionsDataUIC.IsActivated = false;
                    _timer = 0;
                }
            }
            else
            {
                MotionsViewUIC.SetActiveParent(false);
            }
        }
    }
}