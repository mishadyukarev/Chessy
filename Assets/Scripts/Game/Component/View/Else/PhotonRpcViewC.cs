using Photon.Pun;
using UnityEngine;

namespace Scripts.Game
{
    public struct PhotonRpcViewC
    {
        public static GameObject RpcView_GO { get; private set; }
        public static PhotonView PhotonView { get; private set; }

        public PhotonRpcViewC(bool needNew)
        {
            RpcView_GO = new GameObject("RpcView");

            PhotonView = RpcView_GO.AddComponent<PhotonView>();


            //PhotonNetwork.AllocateViewID(PhotonView);
            //PhotonView.ViewID = 1001;
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
