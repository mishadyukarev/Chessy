using Photon.Pun;
using UnityEngine;

namespace Scripts.Game
{
    public struct RpcViewC
    {
        public static GameObject RpcView_GO { get; private set; }
        public static PhotonView PhotonView { get; private set; }

        public RpcViewC(GameObject rpcView_GO)
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
