using UnityEngine;

namespace Chessy.Game
{
    sealed class CenterMotionUIS : SystemUIAbstract, IEcsRunSystem
    {
        float _timer;

        internal CenterMotionUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            if (E.MotionIsActive)
            {
                UIEs.CenterEs.Motion.TextUI.text = E.Motions.ToString();
                UIEs.CenterEs.Motion.SetActiveParent(true);

                _timer += Time.deltaTime;

                if (_timer >= 1)
                {
                    UIEs.CenterEs.Motion.SetActiveParent(false);
                    E.MotionIsActive = false;
                    _timer = 0;
                }
            }
            else
            {
                UIEs.CenterEs.Motion.SetActiveParent(false);
            }
        }
    }
}