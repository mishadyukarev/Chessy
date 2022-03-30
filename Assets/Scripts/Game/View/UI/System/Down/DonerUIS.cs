using Chessy.Game.Entity.Model;
using Chessy.Game.Entity.View.UI.Down;
using Chessy.Game.Enum;
using UnityEngine;

namespace Chessy.Game
{
    sealed class DonerUIS : SystemUIAbstract, IEcsRunSystem
    {
        readonly DonerUIE _donerE;

        internal DonerUIS(in DonerUIE downDoner, in EntitiesModelGame ents) : base(ents)
        {
            _donerE = downDoner;
        }

        public void Run()
        {
            if (!e.LessonTC.HaveLesson || e.LessonTC.LessonT >= LessonTypes.ShiftPawnHere)
            {
                _donerE.ButtonC.SetActive(true);
                _donerE.WaitGoC.SetActive(true);

                if (e.CurPlayerITC.Is(e.WhoseMove.Player))
                {
                    _donerE.WaitGoC.SetActive(false);
                    _donerE.ButtonC.Image.color = Color.white;
                }
                else
                {
                    _donerE.WaitGoC.SetActive(true);
                    _donerE.ButtonC.Image.color = Color.red;
                }
            }
            else
            {
                _donerE.ButtonC.SetActive(false);
                _donerE.WaitGoC.SetActive(false);
            }
        }
    }
}