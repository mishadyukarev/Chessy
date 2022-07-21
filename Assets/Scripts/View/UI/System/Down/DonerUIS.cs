using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Entity.View.UI.Down;
using Chessy.Model.Enum;
using Chessy.View.UI.Entity;
using UnityEngine;

namespace Chessy.View.UI.System
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
            //if (!_aboutGameC.LessonType.HaveLesson() || _aboutGameC.LessonType >= LessonTypes.HoldPressReady)
            //{
            //    _donerE.ButtonC.SetActiveParent(false);
            //    _donerE.WaitGoC.SetActive(false);

            //    if (_aboutGameC.CurrentPlayerIT == PlayerTypes.First)
            //    {
            //        _donerE.WaitGoC.SetActive(false);
            //        _donerE.ImageC.Image.color = Color.white;
            //    }
            //    else
            //    {
            //        _donerE.WaitGoC.SetActive(true);
            //        _donerE.ImageC.Image.color = Color.red;
            //    }
            //}
            //else
            //{
            //    _donerE.ButtonC.SetActiveParent(false);
            //    _donerE.WaitGoC.SetActive(false);
            //}
        }
    }
}