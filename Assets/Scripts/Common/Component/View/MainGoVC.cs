using UnityEngine;

namespace Game.Common
{
    public struct MainGoVC
    {
        private static GameObject _main;

        public static Vector3 Pos => _main.transform.position;
        public static Quaternion Rot => _main.transform.rotation;

        public MainGoVC(GameObject main_GO)
        {
            _main = main_GO;
        }
    }
}