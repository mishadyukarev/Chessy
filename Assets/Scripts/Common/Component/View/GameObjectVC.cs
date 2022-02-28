using UnityEngine;

namespace Chessy.Game
{
    public struct GameObjectVC
    {
        public GameObject GameObject;

        public string Name => GameObject.name;
        public Transform Transform => GameObject.transform;
        public int InstanceID => GameObject.GetInstanceID();
        public bool IsActiveSelf => GameObject.activeSelf;

        public GameObjectVC(in GameObject gO)
        {
            GameObject = gO;
        }

        public void SetActive(in bool needActive) => GameObject.SetActive(needActive);
        public void SetActiveParent(in bool needActive) => GameObject.transform.parent.gameObject.SetActive(needActive);
    }
}