using Chessy.Game.Model.Entity;
using Chessy.Game.Entity.View.UI.Down;
using Chessy.Game.Enum;
using UnityEngine;

namespace Chessy.Game
{
    sealed class DonerUIS : SystemUIAbstract
    {
        readonly DonerUIE _donerE;

        internal DonerUIS(in DonerUIE downDoner, in EntitiesModelGame ents) : base(ents)
        {
            _donerE = downDoner;
        }

        internal override void Sync()
        {
            if (!e.LessonTC.HaveLesson || e.LessonTC.LessonT >= LessonTypes.HoldPressReady)
            {
                _donerE.ButtonC.SetActiveParent(true);
                _donerE.WaitGoC.SetActive(true);

                if (e.CurPlayerITC.Is(e.WhoseMovePlayerTC.PlayerT))
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