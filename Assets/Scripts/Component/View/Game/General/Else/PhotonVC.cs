using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct PhotonVC : IPhoton
    {
        readonly PhotonView _photonView;

        public PhotonVC(in PhotonView photonView, out List<object> actions)
        {
            _photonView = photonView;

            actions = new List<object>();
            actions.Add((Action<string, RpcTarget, object[]>)_photonView.RPC);
            actions.Add((Action<string, Player, object[]>)_photonView.RPC);
        }
    }
}