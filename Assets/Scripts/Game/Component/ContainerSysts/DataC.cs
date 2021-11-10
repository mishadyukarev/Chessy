using System;

namespace Chessy.Game
{
    public struct DataC
    {
        public static Action RunUpdate { get; private set; }

        public DataC(Action run)
        {
            RunUpdate = run;
        }
    }
}

