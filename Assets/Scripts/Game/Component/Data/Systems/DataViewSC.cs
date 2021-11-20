using System;

namespace Game.Game
{
    public struct DataViewSC
    {
        private static Action _runUpdate;
        public static Action RotateAll { get; private set; }

        public DataViewSC(Action runUpdate, Action rotateAll)
        {
            _runUpdate = runUpdate;
            RotateAll = rotateAll;
        }

        public static void RunUpdate() => _runUpdate.Invoke();
    }
}