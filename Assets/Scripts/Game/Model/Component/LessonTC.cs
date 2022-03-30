using Chessy.Game.Enum;

namespace Chessy.Game.Model.Component
{
    public struct LessonTC
    {
        public LessonTypes LessonT;

        public bool HaveLesson => !LessonT.Is(LessonTypes.None, LessonTypes.End);
        public bool Is(params LessonTypes[] lessonTs) => LessonT.Is(lessonTs);

        public void SetNextLesson()
        {
            if (LessonT == LessonTypes.End - 1)
            {
                LessonT = LessonTypes.None;
            }
            else LessonT++;
        }
        public void SetPreviousLesson()
        {
            LessonT--;
        }
    }
}