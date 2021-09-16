using UnityEngine;

namespace Assets.Scripts.ECS.Component
{
    internal struct ComZoneComp
    {
        private GameObject _comZoneGO;

        internal ComZoneComp(GameObject commonZoneGO) => _comZoneGO = commonZoneGO;

        internal void Attach(Transform transform) => transform.SetParent(_comZoneGO.transform);
    }
}
