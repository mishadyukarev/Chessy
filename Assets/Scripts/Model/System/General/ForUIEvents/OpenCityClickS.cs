using Chessy.Model.Enum;
namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void OpenCityClick()
        {
            _dataFromViewC.SoundAction(ClipTypes.Click).Invoke();


            AboutGameC.IsSelectedCity = !AboutGameC.IsSelectedCity;

            if (AboutGameC.LessonT.Is(LessonTypes.OpeningTown))
            {
                _s.SetNextLesson();

            }
            if (AboutGameC.LessonT.Is(LessonTypes.TryBuyingHouse))
            {
                if (!AboutGameC.IsSelectedCity) _s.SetPreviousLesson();
            }



            _updateAllViewC.NeedUpdateView = true;
        }
    }
}