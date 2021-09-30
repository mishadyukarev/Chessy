using UnityEngine;

namespace Assets.Scripts.ECS.Component
{
    public struct ComZoneComp
    {
        private GameObject _comZoneGO;

        public ComZoneComp(GameObject commonZoneGO) => _comZoneGO = commonZoneGO;

        public void Attach(Transform transform) => transform.SetParent(_comZoneGO.transform);
    }
}
