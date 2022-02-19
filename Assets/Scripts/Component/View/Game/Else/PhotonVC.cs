﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public readonly struct PhotonVC : IPhotonE
    {
        readonly PhotonView _photonView;

        public PhotonVC(in PhotonView photonView, out List<object> actions)
        {
            _photonView = photonView;

            actions = new List<object>();
            actions.Add((ActionMy<string, RpcTarget, object[]>)_photonView.RPC);
            actions.Add((ActionMy<string, Player, object[]>)_photonView.RPC);
        }


        public C AddComponent<C>() where C : MonoBehaviour => _photonView.gameObject.AddComponent<C>();
    }
}