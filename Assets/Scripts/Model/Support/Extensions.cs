using Chessy.Game.Enum;
using System;
using System.Linq;

namespace Chessy.Game.Extensions
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