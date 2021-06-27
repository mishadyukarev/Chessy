using UnityEngine;

namespace Assets.Scripts.ECS.Common.Components
{
    internal struct ParentGOZoneComponent
    {
        private GameObject _parentGOZone;

        internal void SetParent(GameObject go) => _parentGOZone = go;
        internal void AttachToCurrentParent(Transform transform) => transform.SetParent(_parentGOZone.transform);
        public void DestroyCurrentGOZone() => GameObject.Destroy(_parentGOZone);
    }
}
