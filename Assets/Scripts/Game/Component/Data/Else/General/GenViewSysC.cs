using System;

namespace Scripts.Game
{
    public struct GenViewSysC
    {
        public static Action RotateAll { get; private set; }

        public GenViewSysC(Action rotateAll)
        {
            RotateAll = rotateAll;
        }
    }
}