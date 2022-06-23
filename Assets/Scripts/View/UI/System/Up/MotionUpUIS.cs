using Chessy.Model.Model.Entity;
using UnityEngine;

namespace Chessy.Model.View.UI
{
    sealed class MotionUpUIS : SystemUIAbstract
    {
        readonly TextUIC _motionTextC;

        internal MotionUpUIS(in TextUIC motionTextC, in EntitiesModel eMGame) : base(eMGame)
        {
            _motionTextC = motionTextC;
        }

        internal override void Sync()
        {
            if (_e.LessonT.HaveLesson())
            {
                _motionTextC.GameObject.SetActive(false);
            }
            else
            {
                _motionTextC.GameObject.SetActive(true);

                _motionTextC.TextUI.text = "Motions: " + _e.Motions.ToString();

                _motionTextC.TextUI.color = _e.Motions % UpdateValues.EVERY_MOTION_FOR_ACTIVE_GOD_ABILITY == 0 && _e.Motions != 0 ? Color.red : Color.white;
            }
        }
    }
}