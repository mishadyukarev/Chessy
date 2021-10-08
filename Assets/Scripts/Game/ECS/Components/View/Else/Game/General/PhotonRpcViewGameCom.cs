using Photon.Pun;
using UnityEngine;

namespace Scripts.Game
{
    internal struct PhotonRpcViewGameCom
    {
        internal static GameObject RpcView_GO { get; private set; }
        internal static PhotonView PhotonView { get; private set; }
        internal static RpcSys RpcSys { get; private set; }

        internal PhotonRpcViewGameCom(bool needNew)
        {
            RpcView_GO = new GameObject("RpcView");

            PhotonView = RpcView_GO.AddComponent<PhotonView>();
            RpcSys = RpcView_GO.AddComponent<RpcSys>();


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
