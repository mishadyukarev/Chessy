using UnityEngine;

namespace Chessy.Game
{
    sealed class CenterMotionUIS : SystemUIAbstract, IEcsRunSystem
    {
        float _timer;

        internal CenterMotionUIS( in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            if (E.MotionIsActive)
            {
                UIE.CenterEs.Motion.TextUI.text = E.Motions.ToString();
                UIE.CenterEs.Motion.SetActiveParent(true);

                _timer += Time.deltaTime;

                if (_timer >= 1)
                {
                    UIE.CenterEs.Motion.SetActiveParent(false);
                    E.MotionIsActive = false;
                    _timer = 0;
                }
            }
            else
            {
                UIE.CenterEs.Motion.SetActiveParent(false);
            }
        }
    }
}