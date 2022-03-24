using Chessy.Game.Enum;

namespace Chessy.Game.Model.Component
{
    public struct LessonTC
    {
        public LessonTypes LessonT;

        public void SetNextLesson() => LessonT++;
    }
}