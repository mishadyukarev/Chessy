using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class Main : MonoBehaviour
{

    #region Variables

    private static Main _instance;

    private Camera _camera;
    private ECSmanager _eCSmanager;
    private PhotonManager _photonManager;
    private SupportManager _supportManager;
    private StartSpawnManager _startSpawnManager;

    #endregion


    #region Properties

    static public Main Instance => _instance;

    internal bool IsMasterClient => PhotonNetwork.IsMasterClient;
    internal Player MasterClient => PhotonNetwork.MasterClient;
    internal Player LocalPlayer => PhotonNetwork.LocalPlayer;

    internal PhotonView PhotonView => _photonManager.PhotonView;

    #endregion



    private void Start()
    {
        _instance = this;
        _supportManager = new SupportManager();


        _startSpawnManager = new StartSpawnManager(_supportManager, out Transform parentTransformScrips);


        _camera = Camera.main;
        if (!IsMasterClient) _camera.transform.Rotate(0, 0, 180);

        _photonManager = new PhotonManager(_supportManager, parentTransformScrips);
        _eCSmanager = new ECSmanager(_supportManager, _photonManager, _startSpawnManager);

        _photonManager.InitAfterECS(_eCSmanager);


        gameObject.transform.SetParent(parentTransformScrips);
    }


    private void Update() => _eCSmanager.Run();

    private void OnDestroy()
    {
        //_photonManager.PhotonManagerScene.LeaveRoom();
    }
}