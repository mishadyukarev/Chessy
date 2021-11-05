using System;

namespace Scripts.Game
{
    public struct GameGenSysDataViewC
    {
        public static Action RunUpdate { get; private set; }
        public static Action RotateAll { get; private set; }

        public GameGenSysDataViewC(Action runUpdate, Action rotateAll)
        {
            RunUpdate = runUpdate;
            RotateAll = rotateAll;
        }
    }
}