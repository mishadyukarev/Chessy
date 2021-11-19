using System;

namespace Game.Common
{
    public struct TimeStartGameComCom
    {
        public static bool WasLikeGameZone;
        public static DateTime TimeStartGame { get; private set; }

        public TimeStartGameComCom(DateTime startTime) => TimeStartGame = startTime;
    }
}