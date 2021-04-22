using Photon.Pun;
using System;
using UnityEngine;
using static MainGame;

public class PhotonManager
{
    private PhotonView _photonView;
    private PhotonPunRPC _photonPunRPC;
    private PhotonManagerScene _photonManagerScene;

    public PhotonView PhotonView => _photonView;
    public PhotonManagerScene PhotonManagerScene => _photonManagerScene;
    public PhotonPunRPC PhotonPunRPC => _photonPunRPC;


    public PhotonManager(SupportManager supportManager, Transform parentTransform)
    {
        var types = new Type[]
        {
            typeof(PhotonView),
            typeof(PhotonManagerScene),
            typeof(PhotonPunRPC)
        };

        var networkGO = supportManager.BuilderManager.CreateGameObject("Network", types, parentTransform);

        _photonView = networkGO.GetPhotonView();
        _photonManagerScene = networkGO.GetComponent<PhotonManagerScene>();
        _photonPunRPC = networkGO.GetComponent<PhotonPunRPC>();

        _photonPunRPC.Constructor(supportManager, this);


        _photonView.FindObservables(true);

        if (Instance.IsMasterClient) PhotonNetwork.AllocateViewID(_photonView);
        else _photonView.ViewID = supportManager.StartValuesConfig.NUMBER_PHOTON_VIEW;
    }

    public void InitAfterECS(ECSmanager eCSmanager)
    {
        _photonPunRPC.InitAfterECS(eCSmanager);
    }
}
