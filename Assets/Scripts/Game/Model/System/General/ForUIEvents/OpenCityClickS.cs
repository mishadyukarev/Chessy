using Chessy.Common.Interface;
using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;

namespace Chessy.Game.System.Model
{
    public sealed class OpenCityClickS : SystemModelGameAbs, IClickUI
    {
        internal OpenCityClickS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        public void Click()
        {
            e.Sound(ClipTypes.Click).Invoke();


            e.IsSelectedCity = !e.IsSelectedCity;

            if (e.LessonTC.Is(LessonTypes.OpeningTown)) e.LessonTC.SetNextLesson();

            if (e.LessonTC.Is(LessonTypes.BuyingHouse))
            {
                if (!e.IsSelectedCity) e.LessonTC.SetPreviousLesson();
            }

            

            

            e.NeedUpdateView = true;
        }
    }
}