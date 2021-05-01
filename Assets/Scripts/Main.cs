﻿using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

internal abstract class Main : MonoBehaviour
{
    internal readonly bool IS_TEST = true;

    protected UnityEvents _unityEvents;


    internal bool IsMasterClient => PhotonNetwork.IsMasterClient;
    internal Player MasterClient => PhotonNetwork.MasterClient;
    internal Player LocalPlayer => PhotonNetwork.LocalPlayer;
}
