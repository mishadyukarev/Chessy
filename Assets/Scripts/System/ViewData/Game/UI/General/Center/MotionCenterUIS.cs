using UnityEngine;
using static Game.Game.EntityCenterUIPool;

namespace Game.Game
{
    struct MotionCenterUIS : IEcsRunSystem
    {
        float _timer;

        public void Run()
        {
            if (EntityPool.MotionZone<IsActiveC>().IsActive)
            {
                Motion<MotionsUIEC>().Text = EntityPool.GameInfo<AmountMotionsC>().Amount.ToString();
                Motion<MotionsUIEC>().SetActiveParent(true);

                _timer += Time.deltaTime;

                if (_timer >= 1)
                {
                    Motion<MotionsUIEC>().SetActiveParent(false);
                    EntityPool.MotionZone<IsActiveC>().IsActive = false;
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