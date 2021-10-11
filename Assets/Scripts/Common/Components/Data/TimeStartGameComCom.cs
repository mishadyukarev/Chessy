using System;

namespace Scripts.Common
{
    public struct TimeStartGameComCom
    {
        public static bool WasLikeGameZone;
        public static DateTime TimeStartGame { get; private set; }

        internal TimeStartGameComCom(DateTime startTime) => TimeStartGame = startTime;
    }
}