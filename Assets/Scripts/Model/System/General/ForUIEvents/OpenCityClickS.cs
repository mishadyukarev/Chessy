using Chessy.Model.Enum;
namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void OpenCityClick()
        {
            _e.SoundAction(ClipTypes.Click).Invoke();


            _e.IsSelectedCity = !_e.IsSelectedCity;

            if (_aboutGameC.LessonT.Is(LessonTypes.OpeningTown))
            {
                _s.SetNextLesson();

            }
            if (_aboutGameC.LessonT.Is(LessonTypes.TryBuyingHouse))
            {
                if (!_e.IsSelectedCity) _s.SetPreviousLesson();
            }



            _updateAllViewC.NeedUpdateView = true;
        }
    }
}