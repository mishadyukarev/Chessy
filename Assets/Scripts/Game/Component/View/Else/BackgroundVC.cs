using UnityEngine;

namespace Chessy.Game
{
    public struct BackgroundVC
    {
        private static GameObject _background_GO;

        public BackgroundVC(GameObject background_GO, bool isMaster)
        {
            background_GO.transform.rotation = isMaster ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);
            _background_GO = background_GO;
        }
    }
}
