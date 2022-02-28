using UnityEngine;

namespace Chessy.Common
{
    public struct ComZoneC
    {
        private static GameObject _comZoneGO;

        public ComZoneC(GameObject commonZoneGO) => _comZoneGO = commonZoneGO;

        public static void Attach(Transform transform) => transform.SetParent(_comZoneGO.transform);
    }
}
