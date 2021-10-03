using UnityEngine;

namespace Scripts.Game
{
    internal struct BackgroundComponent
    {
        private GameObject _background_GO;

        internal BackgroundComponent(GameObject background_GO, bool isMaster)
        {
            background_GO.transform.rotation = isMaster ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);
            _background_GO = background_GO;
        }
    }
}
