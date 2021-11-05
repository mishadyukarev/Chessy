using System;

namespace Scripts.Menu
{
    public struct MenuSysDataC
    {
        public static Action RunUpdate { get; private set; }

        public MenuSysDataC(Action runUpdate)
        {
            RunUpdate = runUpdate;
        }
    }
}