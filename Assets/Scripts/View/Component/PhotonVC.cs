using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

namespace Chessy.Model
{
    public struct PhotonVC
    {
        public PhotonView PhotonView;

        public PhotonVC(in PhotonView photonView, out List<object> actions)
        {
            PhotonView = photonView;

            actions = new List<object>();
            actions.Add((ActionMy<string, RpcTarget, object[]>)PhotonView.RPC);
            actions.Add((ActionMy<string, Player, object[]>)PhotonView.RPC);
        }
    }
}