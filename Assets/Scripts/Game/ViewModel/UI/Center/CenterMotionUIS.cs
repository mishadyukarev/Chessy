using UnityEngine;

namespace Chessy.Game
{
    static class CenterMotionUIS
    {
        static float _timer;

        public static void Run(in float timer, in EntitiesViewUI eUI, in EntitiesModel e)
        {
            if (e.MotionIsActive)
            {
                eUI.CenterEs.Motion.TextUI.text = e.Motions.ToString();
                eUI.CenterEs.Motion.SetActiveParent(true);

                _timer += Time.deltaTime + timer;

                if (_timer >= 1)
                {
                    eUI.CenterEs.Motion.SetActiveParent(false);
                    e.MotionIsActive = false;
                    _timer = 0;
                }
            }
            else
            {
                eUI.CenterEs.Motion.SetActiveParent(false);
            }
        }
    }
}