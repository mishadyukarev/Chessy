using UnityEngine;

namespace Chessy.Common
{
    public struct ComZoneC
    {
        private GameObject _comZoneGO;

        public ComZoneC(GameObject commonZoneGO) => _comZoneGO = commonZoneGO;

        public void Attach(Transform transform) => transform.SetParent(_comZoneGO.transform);
    }
}
