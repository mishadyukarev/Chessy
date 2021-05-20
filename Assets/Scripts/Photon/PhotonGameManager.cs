using JetBrains.Annotations;
using Photon.Pun;
using System;
using UnityEngine;
using static MainGame;

internal class PhotonGameManager
{
    private PhotonView _photonView;
    private PhotonPunRPC _photonPunRPC;
    private GameSceneManager _gameSceneManager;

    internal GameSceneManager GameSceneManager => _gameSceneManager;
    internal PhotonPunRPC PhotonPunRPC => _photonPunRPC;


    internal PhotonGameManager([NotNull]Transform parentTransform)
    {
        var types = new Type[]
        {
            typeof(PhotonView),
            typeof(GameSceneManager),
            typeof(PhotonPunRPC)
        };

        var networkGO = Instance.Builder.CreateGameObject("Network", types, parentTransform);

        _photonView = networkGO.GetPhotonView();
        _gameSceneManager = networkGO.GetComponent<GameSceneManager>();
        _photonPunRPC = networkGO.GetComponent<PhotonPunRPC>();

        _photonPunRPC.Constructor(_photonView);


        _photonView.FindObservables(true);

        if (Instance.IsMasterClient) PhotonNetwork.AllocateViewID(_photonView);
        else _photonView.ViewID = Instance.StartValuesGameConfig.NUMBER_PHOTON_VIEW;
    }
}
