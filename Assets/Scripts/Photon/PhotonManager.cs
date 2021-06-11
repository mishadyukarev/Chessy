using Photon.Pun;
using static Main;

internal class PhotonManager
{
    private PhotonView _photonView;
    private PhotonPunRPC _photonPunRPC;
    private SceneManager _sceneManager;

    internal SceneManager SceneManager => _sceneManager;
    internal PhotonPunRPC PhotonPunRPC => _photonPunRPC;


    internal PhotonManager()
    {
        Instance.gameObject.AddComponent<PhotonView>();
        Instance.gameObject.AddComponent<SceneManager>();
        Instance.gameObject.AddComponent<PhotonPunRPC>();

        _photonView = Instance.gameObject.GetPhotonView();
        _sceneManager = Instance.gameObject.GetComponent<SceneManager>();
        _photonPunRPC = Instance.gameObject.GetComponent<PhotonPunRPC>();

        _photonPunRPC.Constructor(_photonView);


        _photonView.FindObservables(true);

        if (Instance.IsMasterClient) PhotonNetwork.AllocateViewID(_photonView);
        else _photonView.ViewID = Instance.StartValuesGameConfig.NUMBER_PHOTON_VIEW;
    }
}
