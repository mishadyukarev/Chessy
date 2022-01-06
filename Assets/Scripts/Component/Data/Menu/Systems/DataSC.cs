using System;

namespace Game.Menu
{
    public struct DataSC
    {
        private static Action _runUpdate;

        public DataSC(Action runUpdate)
        {
            _runUpdate = runUpdate;
        }

        public static void RunUpdate() => _runUpdate.Invoke();
    }
}