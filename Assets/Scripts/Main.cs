﻿using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

internal abstract class Main : MonoBehaviour
{
    protected Camera _camera;

    internal bool IsMasterClient => PhotonNetwork.IsMasterClient;
    internal Player MasterClient => PhotonNetwork.MasterClient;
    internal Player LocalPlayer => PhotonNetwork.LocalPlayer;
}
