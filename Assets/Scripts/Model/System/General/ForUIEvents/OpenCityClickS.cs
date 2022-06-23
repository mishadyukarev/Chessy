﻿using Chessy.Model.Enum;

namespace Chessy.Model.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        public void OpenCityClick()
        {
            _e.SoundAction(ClipTypes.Click).Invoke();


            _e.IsSelectedCity = !_e.IsSelectedCity;

            if (_e.LessonT.Is(LessonTypes.OpeningTown))
            {
                _e.LessonT.SetNextLesson();

            }
            if (_e.LessonT.Is(LessonTypes.TryBuyingHouse))
            {
                if (!_e.IsSelectedCity) _e.LessonT.SetPreviousLesson();
            }



            _e.NeedUpdateView = true;
        }
    }
}