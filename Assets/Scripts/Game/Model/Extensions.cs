using Chessy.Game.Enum;
using System;
using System.Linq;

namespace Chessy.Game.Model
{
    public static class Extensions
    {
        public static bool Is(this LessonTypes curLessonT, params LessonTypes[] lessonTs)
        {
            if (lessonTs == default) throw new Exception();

            return lessonTs.Any((LessonTypes lessonT) => lessonT == curLessonT);
        }
    }
}