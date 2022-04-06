using Chessy.Game.Entity.View.UI.Down;
using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using UnityEditor;
using UnityEngine;

namespace Chessy.Game
{
    sealed class CityButtonUIS : SystemUIAbstract
    {
        readonly CityButtonUIE _cityButtonUIE;

        internal CityButtonUIS(in CityButtonUIE cityButtonUIE, in EntitiesModelGame eMG) : base(eMG)
        {
            _cityButtonUIE = cityButtonUIE;
        }

        internal override void Sync()
        {
            if (!e.LessonTC.HaveLesson || e.LessonT >= LessonTypes.OpeningTown)
            {
                _cityButtonUIE.ParentGOC.SetActive(true);
            }
            else
            {
                _cityButtonUIE.ParentGOC.SetActive(false);
            }
        }
    }
}