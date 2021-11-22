using UnityEngine;

namespace Game.Common
{
    public struct MainGoVC
    {
        private static GameObject _main;

        public static Vector3 Pos => _main.transform.position;
        public static Quaternion Rot => _main.transform.rotation;
        public static Transform Trans => _main.transform;

        public MainGoVC(GameObject main_GO)
        {
            _main = main_GO;
        }

        public static T AddComponent<T>() where T : Component => _main.AddComponent<T>();
    }
}