using System;

namespace Chessy.Common
{
    public readonly struct TimeStartGameC
    {
        public readonly DateTime TimeStartGame;

        internal TimeStartGameC(DateTime startTime) => TimeStartGame = startTime;
    }
}