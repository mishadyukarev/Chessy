using System;

namespace Game.Game
{
    public struct DataSC
    {
        private static Action _runUpdate;

        public DataSC(Action run)
        {
            _runUpdate = run;
        }

        public static void RunUpdate() => _runUpdate.Invoke();
    }
}

