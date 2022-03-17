using UnityEngine;

namespace Chessy.Game
{
    static class MotionUIS
    {
        static float _timer;

        public static void Sync(in float timer, in EntitiesViewUI eUI, in EntitiesModel e)
        {
            if (e.ZoneInfoC.MotionIsActive)
            {
                eUI.CenterEs.Motion.TextUI.text = e.MotionsC.Motions.ToString();
                eUI.CenterEs.Motion.SetActiveParent(true);

                _timer += Time.deltaTime + timer;

                if (_timer >= 4)
                {
                    eUI.CenterEs.Motion.SetActiveParent(false);
                    e.ZoneInfoC.MotionIsActive = false;
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