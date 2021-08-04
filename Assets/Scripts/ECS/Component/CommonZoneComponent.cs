using UnityEngine;

namespace Assets.Scripts.ECS.Component
{
    internal struct CommonZoneComponent
    {
        private GameObject _commonZoneGO;

        internal CommonZoneComponent(GameObject commonZoneGO) => _commonZoneGO = commonZoneGO;

        internal void Attach(Transform transform) => transform.SetParent(_commonZoneGO.transform);
    }
}
