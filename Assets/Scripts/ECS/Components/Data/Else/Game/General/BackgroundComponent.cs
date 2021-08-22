using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts.ECS.Component.Game
{
    internal struct BackgroundComponent
    {
        private GameObject _background_GO;

        internal BackgroundComponent(GameObject background_GO)
        {
            background_GO.transform.rotation = PhotonNetwork.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);
            _background_GO = background_GO;
        }
    }
}
