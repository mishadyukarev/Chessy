using UnityEngine;
using static Game.Game.EntityCenterUIPool;

namespace Game.Game
{
    struct MotionCenterUISystem : IEcsRunSystem
    {
        private float _timer;

        public void Run()
        {
            if (MotionsC.IsActivated)
            {
                Motion<MotionsUIEC>().Text = MotionsC.AmountMotions.ToString();
                Motion<MotionsUIEC>().SetActiveParent(true);

                _timer += Time.deltaTime;

                if (_timer >= 1)
                {
                    Motion<MotionsUIEC>().SetActiveParent(false);
                    MotionsC.IsActivated = false;
                    _timer = 0;
                }
            }
            else
            {
                Motion<MotionsUIEC>().SetActiveParent(false);
            }
        }
    }
}