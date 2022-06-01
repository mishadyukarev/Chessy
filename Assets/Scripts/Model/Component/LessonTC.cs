using Chessy.Game.Enum;
using Chessy.Game.Extensions;

namespace Chessy.Game.Model.Component
{
    public struct LessonTC
    {
        public LessonTypes LessonT { get; internal set; }

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
        internal void SetPreviousLesson()
        {
            LessonT--;
        }
        public void EndLesson()
        {
            LessonT = LessonTypes.None;
        }
    }
}