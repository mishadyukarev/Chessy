using UnityEngine;

namespace Scripts.Common
{
    public struct MainGOComC
    {
        public static GameObject Main_GO { get; private set; }

        public MainGOComC(GameObject main_GO)
        {
            Main_GO = main_GO;
        }
    }
}