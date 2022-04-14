using Chessy.Common.Enum;
using Chessy.Common.Interface;
using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    public sealed class OpenCityClickS : SystemModel, IClickUI
    {
        internal OpenCityClickS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        public void Click()
        {
            eMG.Common.SoundActionC(ClipCommonTypes.Click).Invoke();


            eMG.IsSelectedCity = !eMG.IsSelectedCity;

            if (eMG.LessonTC.Is(LessonTypes.OpeningTown, LessonTypes.ClickOpenTown2))
            {
                eMG.LessonTC.SetNextLesson();

            }
            if (eMG.LessonTC.Is(LessonTypes.BuyingHouse, LessonTypes.ClickBuyMelterInTown))
            {
                if (!eMG.IsSelectedCity) eMG.LessonTC.SetPreviousLesson();
            }



            eMG.NeedUpdateView = true;
        }
    }
}