using Photon.Pun;
using UnityEngine;

internal sealed class MainGame : Main
{

    #region Variables

    private static MainGame _instanceGame;

    private PhotonGameManager _photonManager;
    private ECSmanager _eCSmanager;
    private SupportGameManager _supportGameManager;
    private StartSpawnGameManager _startSpawnManager;

    private bool _isStartedGame;

    #endregion


    #region Properties

    public static MainGame InstanceGame => _instanceGame;

    internal bool IsStartedGame
    {
        get { return _isStartedGame; }
        set { _isStartedGame = value; }
    }

    #endregion


    private void Start()
    {
        _instanceGame = this;
        _supportGameManager = new SupportGameManager();


        _startSpawnManager = new StartSpawnGameManager(_supportGameManager, out Transform parentTransformScrips);


        _photonManager = new PhotonGameManager(_supportGameManager, parentTransformScrips);
        _eCSmanager = new ECSmanager(_supportGameManager, _photonManager, _startSpawnManager);

        _photonManager.InitAfterECS(_eCSmanager);


        gameObject.transform.SetParent(parentTransformScrips);
    }


    private void Update()
    {
        _eCSmanager.Run();
    }

    private void OnDestroy() { }
}