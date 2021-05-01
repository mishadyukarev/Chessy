using UnityEngine;

internal sealed class MainGame : Main
{

    #region Variables

    private static MainGame _instanceGame;

    private SupportGameManager _supportGameManager;

    private StartValuesGameConfig _startValuesGameConfig;
    private StartSpawnGame _startSpawnGame;
    private PhotonGameManager _photonGameManager;
    private ECSmanager _eCSmanager;

    private Transform _parentTransformScrips;

    #endregion


    #region Properties

    public static MainGame InstanceGame => _instanceGame;

    internal SupportGameManager SupportGameManager => _supportGameManager;
    internal StartValuesGameConfig StartValuesGameConfig => _startValuesGameConfig;
    internal StartSpawnGame StartSpawnGameManager => _startSpawnGame;
    internal PhotonGameManager PhotonGameManager => _photonGameManager;

    #endregion


    private void Awake()
    {
        _instanceGame = this;
        _supportGameManager = new SupportGameManager();

        _startValuesGameConfig = _supportGameManager.ResourcesLoadGameManager.StartValuesConfig;
        _startSpawnGame = new StartSpawnGame(_supportGameManager, out _parentTransformScrips);

        _unityEvents = new UnityEvents(_supportGameManager.Builder);
        gameObject.transform.SetParent(_parentTransformScrips);

        _photonGameManager = new PhotonGameManager(_parentTransformScrips);
    }

    private void Start()
    {
        _eCSmanager = new ECSmanager();
        _photonGameManager.PhotonPunRPC.InitAfterECS(_eCSmanager);
    }


    private void Update()
    {
        _eCSmanager.Update();
    }

    private void OnDestroy() { }
}