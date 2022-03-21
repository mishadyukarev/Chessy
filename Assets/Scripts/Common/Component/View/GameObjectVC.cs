using UnityEngine;

namespace Chessy.Common.Component
{
    public struct GameObjectVC
    {
        public GameObject GameObject;


        public Transform Transform => GameObject.transform;

        public int InstanceID => GameObject.GetInstanceID();
        public bool IsActiveSelf => GameObject.activeSelf;

        public GameObjectVC(in GameObject gO)
        {
            GameObject = gO;
        }

        public void SetActive(in bool needActive)
        {
            if (needActive != GameObject.activeSelf)
                GameObject.SetActive(needActive);
        }
        public void SetActiveParent(in bool needActive) => GameObject.transform.parent.gameObject.SetActive(needActive);
    }
}