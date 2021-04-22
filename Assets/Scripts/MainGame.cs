using UnityEngine;

internal sealed class MainGame : Main
{

    #region Variables

    private static MainGame _instanceGame;

    private PhotonManager _photonManager;
    private ECSmanager _eCSmanager;
    private SupportGameManager _supportManager;
    private StartSpawnManager _startSpawnManager;

    #endregion


    #region Properties

    public static MainGame InstanceGame => _instanceGame;

    #endregion



    private void Start()
    {
        _instanceGame = this;
        _supportManager = new SupportGameManager();

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