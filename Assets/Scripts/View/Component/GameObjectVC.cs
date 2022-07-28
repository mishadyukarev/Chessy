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

        public void TrySetActive2(in bool needActive, ref bool wasActivated)
        {
            if (needActive != wasActivated) GO.SetActive(needActive);

            wasActivated = needActive;
        }
    }
}