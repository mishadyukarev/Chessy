﻿using System;

namespace Game.Common
{
    public struct TimeStartGameC
    {
        public static bool WasLikeGameZone;
        public static DateTime TimeStartGame { get; private set; }

        public TimeStartGameC(DateTime startTime) => TimeStartGame = startTime;
    }
}