using UnityEngine;

namespace Scripts.Common
{
    public struct MainGoVC
    {
        public static GameObject Main_GO { get; private set; }

        public MainGoVC(GameObject main_GO)
        {
            Main_GO = main_GO;
        }
    }
}