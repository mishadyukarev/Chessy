using Chessy.Model.Enum;
namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void OpenCityClick()
        {
            dataFromViewC.SoundAction(ClipTypes.Click).Invoke();


            aboutGameC.IsSelectedCity = !aboutGameC.IsSelectedCity;

            if (aboutGameC.LessonT.Is(LessonTypes.OpeningTown))
            {
                s.SetNextLesson();

            }
            if (aboutGameC.LessonT.Is(LessonTypes.TryBuyingHouse))
            {
                if (!aboutGameC.IsSelectedCity) s.SetPreviousLesson();
            }



            updateAllViewC.NeedUpdateView = true;
        }
    }
}