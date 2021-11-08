using System;

namespace Chessy.Game
{
    public struct GameGenSysDataC
    {
        public static Action RunUpdate { get; private set; }

        public GameGenSysDataC(Action run)
        {
            RunUpdate = run;
        }
    }
}

