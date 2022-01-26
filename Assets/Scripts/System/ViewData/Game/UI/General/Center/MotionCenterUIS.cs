using UnityEngine;
using static Game.Game.EntityCenterUIPool;

namespace Game.Game
{
    struct MotionCenterUIS : IEcsRunSystem
    {
        float _timer;

        public void Run()
        {
            if (Entities.Motion.IsActiveC.IsActive)
            {
                Motion<MotionsUIEC>().Text = Entities.Motion.AmountMotions.Amount.ToString();
                Motion<MotionsUIEC>().SetActiveParent(true);

                _timer += Time.deltaTime;

                if (_timer >= 1)
                {
                    Motion<MotionsUIEC>().SetActiveParent(false);
                    Entities.Motion.IsActiveC.IsActive = false;
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