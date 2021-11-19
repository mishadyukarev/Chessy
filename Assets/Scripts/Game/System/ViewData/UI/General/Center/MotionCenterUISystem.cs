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
                MotionsViewUIC.Text = MotionsC.AmountMotions.ToString();
                MotionsViewUIC.SetActiveParent(true);

                _timer += Time.deltaTime;

                if (_timer >= 1)
                {
                    MotionsViewUIC.SetActiveParent(false);
                    MotionsC.IsActivated = false;
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