using Chessy.Game.Enum;

namespace Chessy.Game.Model.Component
{
    public struct LessonTC
    {
        public LessonTypes LessonT;

        public bool HaveLesson => LessonT != LessonTypes.None || LessonT != LessonTypes.End;

        public void SetNextLesson() => LessonT++;
    }
}