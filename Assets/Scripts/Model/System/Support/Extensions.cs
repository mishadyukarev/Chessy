using Chessy.Model.Enum;
using System;
using System.Linq;

namespace Chessy.Model.Extensions
{
    public static class Extensions
    {
        public static bool Is(this LessonTypes curLessonT, params LessonTypes[] lessonTs)
        {
            if (lessonTs == default) throw new Exception();

            return lessonTs.Any((LessonTypes lessonT) => lessonT == curLessonT);
        }

        public static PlayerTypes NextPlayer(this PlayerTypes playerT) => playerT == PlayerTypes.First ? PlayerTypes.Second : PlayerTypes.First;
    }
}