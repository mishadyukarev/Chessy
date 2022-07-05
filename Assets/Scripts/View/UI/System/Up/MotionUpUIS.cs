using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.UI.Component;
using UnityEngine;
namespace Chessy.View.UI.System
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

                _motionTextC.TextUI.color = _e.Motions % ValuesChessy.EVERY_MOTION_FOR_ACTIVE_GOD_ABILITY == 0 && _e.Motions != 0 ? Color.red : Color.white;
            }
        }
    }
}