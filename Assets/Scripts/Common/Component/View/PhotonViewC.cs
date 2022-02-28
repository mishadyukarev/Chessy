using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public struct PhotonViewC
    {
        public PhotonView PhotonView;

        public PhotonViewC(in PhotonView photonView, out List<object> actions)
        {
            PhotonView = photonView;

            actions = new List<object>();
            actions.Add((ActionMy<string, RpcTarget, object[]>)PhotonView.RPC);
            actions.Add((ActionMy<string, Player, object[]>)PhotonView.RPC);
        }


        public C AddComponent<C>() where C : MonoBehaviour => PhotonView.gameObject.AddComponent<C>();
    }
}