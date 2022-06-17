using Chessy.Common.Enum;
using Chessy.Game.Enum;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        public void OpenCityClick()
        {
            _eMG.Common.SoundActionC(ClipCommonTypes.Click).Invoke();


            _eMG.IsSelectedCity = !_eMG.IsSelectedCity;

            if (_eMG.LessonTC.Is(LessonTypes.OpeningTown))
            {
                _eMG.LessonTC.SetNextLesson();

            }
            if (_eMG.LessonTC.Is(LessonTypes.TryBuyingHouse))
            {
                if (!_eMG.IsSelectedCity) _eMG.LessonTC.SetPreviousLesson();
            }



            _eMG.NeedUpdateView = true;
        }
    }
}