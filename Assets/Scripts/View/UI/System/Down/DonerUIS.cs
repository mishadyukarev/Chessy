using Chessy.Model.Entity.View.UI.Down;
using Chessy.Model.Enum;
using Chessy.Model;
using UnityEngine;

namespace Chessy.Model
{
    sealed class DonerUIS : SystemUIAbstract
    {
        readonly DonerUIE _donerE;

        internal DonerUIS(in DonerUIE downDoner, in EntitiesModel ents) : base(ents)
        {
            _donerE = downDoner;
        }

        internal override void Sync()
        {
            if (!_e.LessonT.HaveLesson() || _e.LessonT >= LessonTypes.HoldPressReady)
            {
                _donerE.ButtonC.SetActiveParent(true);
                _donerE.WaitGoC.SetActive(true);

                if (_e.CurPlayerIT == _e.WhoseMovePlayerT)
                {
                    _donerE.WaitGoC.SetActive(false);
                    _donerE.ImageC.Image.color = Color.white;
                }
                else
                {
                    _donerE.WaitGoC.SetActive(true);
                    _donerE.ImageC.Image.color = Color.red;
                }
            }
            else
            {
                _donerE.ButtonC.SetActiveParent(false);
                _donerE.WaitGoC.SetActive(false);
            }
        }
    }
}