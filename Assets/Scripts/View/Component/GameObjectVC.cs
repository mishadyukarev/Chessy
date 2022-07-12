using UnityEngine;

namespace Chessy.View.Component
{
    public readonly struct GameObjectVC
    {
        public readonly GameObject GO;

        public Transform Transform => GO.transform;
        public bool IsActiveSelf => GO.activeSelf;

        public GameObjectVC(in GameObject gO)
        {
            GO = gO;
        }

        public void TrySetActive(in bool needActive)
        {
            if (needActive != GO.activeSelf) GO.SetActive(needActive);
        }
    }
}