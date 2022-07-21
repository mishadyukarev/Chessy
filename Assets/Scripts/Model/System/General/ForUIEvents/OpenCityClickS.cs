using Chessy.Model.Enum;
namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void OpenCityClick()
        {
            _dataFromViewC.SoundAction(ClipTypes.Click).Invoke();


            _aboutGameC.IsSelectedCity = !_aboutGameC.IsSelectedCity;

            if (_aboutGameC.LessonT.Is(LessonTypes.OpeningTown))
            {
                _s.SetNextLesson();

            }
            if (_aboutGameC.LessonT.Is(LessonTypes.TryBuyingHouse))
            {
                if (!_aboutGameC.IsSelectedCity) _s.SetPreviousLesson();
            }



            _updateAllViewC.NeedUpdateView = true;
        }
    }
}