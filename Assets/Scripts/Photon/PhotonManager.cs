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
        else _photonView.ViewID = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.StartValuesGameConfig.NUMBER_PHOTON_VIEW;
    }

    internal void ToggleScene(SceneTypes sceneType)
    {
        switch (sceneType)
        {
            case SceneTypes.Menu:
                _sceneManager.ToggleScene(sceneType);
                break;

            case SceneTypes.Game:
                _sceneManager.ToggleScene(sceneType);
                _photonPunRPC.RefreshAllToMaster();
                break;

            default:
                break;
        }
    }
}
