using Chessy.Model.Enum;

namespace Chessy.Model
{
    public sealed partial class SystemsModelGameForUI
    {
        public void OpenCityClick()
        {
            _e.SoundAction(ClipTypes.Click).Invoke();


            _e.IsSelectedCity = !_e.IsSelectedCity;

            if (_e.LessonT.Is(LessonTypes.OpeningTown))
            {
                _e.CommonInfoAboutGameC.SetNextLesson();

            }
            if (_e.LessonT.Is(LessonTypes.TryBuyingHouse))
            {
                if (!_e.IsSelectedCity) _e.CommonInfoAboutGameC.SetPreviousLesson();
            }



            _e.NeedUpdateView = true;
        }
    }
}