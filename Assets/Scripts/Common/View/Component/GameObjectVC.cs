using UnityEngine;

namespace Chessy.Common.Component
{
    public struct GameObjectVC
    {
        public readonly int InstanceID;
        public GameObject GameObject;

        public Transform Transform => GameObject.transform;
        public bool IsActiveSelf => GameObject.activeSelf;

        public GameObjectVC(in GameObject gO)
        {
            GameObject = gO;
            InstanceID = gO.GetInstanceID();
        }

        public void SetActive(in bool needActive)
        {
            if (needActive != GameObject.activeSelf)
                GameObject.SetActive(needActive);
        }
    }
}