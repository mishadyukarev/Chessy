﻿using UnityEngine;

namespace Chessy.Game
{
    static class MotionUIS
    {
        static float _timer;

        public static void Sync(in float timer, in EntitiesViewUI eUI, in Chessy.Game.Entity.Model.EntitiesModel e)
        {
            if (e.ZoneInfoC.IsActiveMotion)
            {
                eUI.CenterEs.Motion.TextUI.text = e.MotionsC.Motions.ToString();
                eUI.CenterEs.Motion.SetActiveParent(true);

                _timer += Time.deltaTime + timer;

                if (_timer >= 4)
                {
                    eUI.CenterEs.Motion.SetActiveParent(false);
                    e.ZoneInfoC.IsActiveMotion = false;
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