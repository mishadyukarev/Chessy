using JetBrains.Annotations;
using Photon.Pun;
using System;
using UnityEngine;
using static MainGame;

public class PhotonGameManager
{
    private PhotonView _photonView;
    private PhotonPunRPC _photonPunRPC;
    private PhotonManagerScene _photonManagerScene;

    internal PhotonManagerScene PhotonManagerScene => _photonManagerScene;
    internal PhotonPunRPC PhotonPunRPC => _photonPunRPC;


    internal PhotonGameManager([NotNull]Transform parentTransform)
    {
        var types = new Type[]
        {
            typeof(PhotonView),
            typeof(PhotonManagerScene),
            typeof(PhotonPunRPC)
        };

        var networkGO = InstanceGame.SupportGameManager.Builder.CreateGameObject("Network", types, parentTransform);

        _photonView = networkGO.GetPhotonView();
        _photonManagerScene = networkGO.GetComponent<PhotonManagerScene>();
        _photonPunRPC = networkGO.GetComponent<PhotonPunRPC>();

        _photonPunRPC.Constructor(_photonView);


        _photonView.FindObservables(true);

        if (InstanceGame.IsMasterClient) PhotonNetwork.AllocateViewID(_photonView);
        else _photonView.ViewID = InstanceGame.StartValuesGameConfig.NUMBER_PHOTON_VIEW;
    }

    public void InitAfterECS(ECSmanager eCSmanager)
    {
        _photonPunRPC.InitAfterECS(eCSmanager);
    }
}
