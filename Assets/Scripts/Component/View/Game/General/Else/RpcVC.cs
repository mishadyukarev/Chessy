using Photon.Pun;
using UnityEngine;

namespace Game.Game
{
    public struct RpcVC
    {
        public static GameObject RpcView_GO { get; private set; }
        public static PhotonView PhotonView { get; private set; }

        public RpcVC(GameObject rpcView_GO)
        {
            RpcView_GO = rpcView_GO;
            PhotonView = RpcView_GO.AddComponent<PhotonView>();

            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.AllocateViewID(PhotonView);
            }
            else
            {
                PhotonView.ViewID = 1001;
            }
        }
    }
}
