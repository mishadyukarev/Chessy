using Chessy.Game.Model.Entity;
using UnityEngine;

namespace Chessy.Game.View.UI
{
    sealed class MotionUpUIS : SystemUIAbstract, IEcsRunSystem
    {
        readonly TextUIC _motionTextC;

        internal MotionUpUIS(in TextUIC motionTextC, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _motionTextC = motionTextC;
        }

        public void Run()
        {
            if (e.LessonTC.HaveLesson)
            {
                _motionTextC.GameObject.SetActive(false);
            }
            else
            {
                _motionTextC.GameObject.SetActive(true);

                _motionTextC.TextUI.text = "Motions: " + e.Motions.ToString();

                _motionTextC.TextUI.color = e.Motions % UpdateValues.EVERY_MOTION_FOR_ACTIVE_GOD_ABILITY == 0 && e.Motions != 0 ? Color.red : Color.white;
            }
        }
    }
}