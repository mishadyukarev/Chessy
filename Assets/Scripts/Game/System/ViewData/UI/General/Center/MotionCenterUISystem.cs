using Leopotam.Ecs;
using UnityEngine;

namespace Game.Game
{
    public sealed class MotionCenterUISystem : IEcsRunSystem
    {
        private float _timer;

        public void Run()
        {
            if (MotionsC.IsActivated)
            {
                MotionsUIC.Text = MotionsC.AmountMotions.ToString();
                MotionsUIC.SetActiveParent(true);

                _timer += Time.deltaTime;

                if (_timer >= 1)
                {
                    MotionsUIC.SetActiveParent(false);
                    MotionsC.IsActivated = false;
                    _timer = 0;
                }
            }
            else
            {
                MotionsUIC.SetActiveParent(false);
            }
        }
    }
}