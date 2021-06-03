using JetBrains.Annotations;
using Photon.Pun;
using UnityEngine;
using static Main;

internal class PhotonManager
{
    private PhotonView _photonView;
    private PhotonPunRPC _photonPunRPC;
    private SceneManager _sceneManager;

    internal SceneManager SceneManager => _sceneManager;
    internal PhotonPunRPC PhotonPunRPC => _photonPunRPC;


    internal PhotonManager([NotNull]GameObject mainGO, CanvasMenuManager canvasMenuManager)
    {
        mainGO.AddComponent<PhotonView>();
        mainGO.AddComponent<SceneManager>();
        mainGO.AddComponent<PhotonPunRPC>();

        _photonView = mainGO.GetPhotonView();
        _sceneManager = mainGO.GetComponent<SceneManager>();
        _photonPunRPC = mainGO.GetComponent<PhotonPunRPC>();

        _photonPunRPC.Constructor(_photonView);


        _photonView.FindObservables(true);

        if (Instance.IsMasterClient) PhotonNetwork.AllocateViewID(_photonView);
        else _photonView.ViewID = Instance.StartValuesGameConfig.NUMBER_PHOTON_VIEW;


        _sceneManager.InitMenu(canvasMenuManager);
    }
}
