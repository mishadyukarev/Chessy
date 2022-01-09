using UnityEngine;

namespace Game.Game
{
    sealed class MotionCenterUISystem : IEcsRunSystem
    {
        private float _timer;

        public void Run()
        {
            if (MotionsC.IsActivated)
            {
                EntityUIPool.MotionCenter<MotionsUIC>().Text = MotionsC.AmountMotions.ToString();
                EntityUIPool.MotionCenter<MotionsUIC>().SetActiveParent(true);

                _timer += Time.deltaTime;

                if (_timer >= 1)
                {
                    EntityUIPool.MotionCenter<MotionsUIC>().SetActiveParent(false);
                    MotionsC.IsActivated = false;
                    _timer = 0;
                }
            }
            else
            {
                EntityUIPool.MotionCenter<MotionsUIC>().SetActiveParent(false);
            }
        }
    }
}