using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public sealed class Main : MonoBehaviour
{

    #region Variables

    static private Main _instance;

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

    #endregion



    private void Start()
    {
        _instance = this;

        _camera = Camera.main;
        if (!IsMasterClient) _camera.transform.Rotate(0, 0, 180);

        _supportManager = new SupportManager();


        _startSpawnManager = new StartSpawnManager(_supportManager, out Transform parentTransformScrips);


        _photonManager = new PhotonManager(_supportManager, parentTransformScrips);
        _eCSmanager = new ECSmanager(_supportManager, _photonManager, _startSpawnManager);

        _photonManager.InitAfterECS(_eCSmanager);


        gameObject.transform.SetParent(parentTransformScrips);
    }


    private void Update()
    {
        _eCSmanager.Run();
    }

    private void OnDestroy()
    {
        _eCSmanager.OnDestroy();
    }
}