using Photon.Pun;
using static Main;

internal sealed class PhotonManager
{
    private PhotonView _photonView;
    private PhotonPunRPC _photonPunRPC;
    private SceneManager _sceneManager;

    internal SceneManager SceneManager => _sceneManager;
    internal PhotonPunRPC PhotonPunRPC => _photonPunRPC;


    internal PhotonManager(ECSManager eCSManager)
    {
        _photonView = Instance.gameObject.AddComponent<PhotonView>();
        _sceneManager = Instance.gameObject.AddComponent<SceneManager>();
        _photonPunRPC = Instance.gameObject.AddComponent<PhotonPunRPC>();

        _photonPunRPC.Constructor(_photonView, eCSManager);

        _photonView.FindObservables(true);

        if (Instance.IsMasterClient) PhotonNetwork.AllocateViewID(_photonView);
        else _photonView.ViewID = Instance.ECSmanagerGame.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.StartValuesGameConfig.NUMBER_PHOTON_VIEW;
    }
}
