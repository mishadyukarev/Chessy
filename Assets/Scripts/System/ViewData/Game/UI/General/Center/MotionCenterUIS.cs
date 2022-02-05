using UnityEngine;
using static Game.Game.EntityCenterUIPool;

namespace Game.Game
{
    sealed class MotionCenterUIS : SystemViewAbstract, IEcsRunSystem
    {
        float _timer;

        public MotionCenterUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            if (Es.Motion.IsActiveC.IsActive)
            {
                Motion<MotionsUIEC>().Text = Es.Motion.AmountMotionsC.Amount.ToString();
                Motion<MotionsUIEC>().SetActiveParent(true);

                _timer += Time.deltaTime;

                if (_timer >= 1)
                {
                    Motion<MotionsUIEC>().SetActiveParent(false);
                    Es.Motion.IsActiveC.IsActive = false;
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