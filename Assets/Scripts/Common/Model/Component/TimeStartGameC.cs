using System;

namespace Chessy.Common
{
    public struct TimeStartGameC
    {
        public readonly DateTime TimeStartGame;

        public TimeStartGameC(DateTime startTime) => TimeStartGame = startTime;
    }
}