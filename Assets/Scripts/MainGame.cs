using UnityEngine;

internal sealed class MainGame : Main
{

    #region Variables

    internal readonly bool IS_TEST = true;

    private static MainGame _instanceGame;

    private SupportGameManager _supportGameManager;

    private StartValuesGameConfig _startValuesGameConfig;
    private StartSpawnGame _startSpawnGameManager;
    private PhotonGameManager _photonManager;
    private ECSmanager _eCSmanager;

    private Transform _parentTransformScrips;

    #endregion


    #region Properties

    public static MainGame InstanceGame => _instanceGame;

    internal SupportGameManager SupportGameManager => _supportGameManager;
    internal StartValuesGameConfig StartValuesGameConfig => _startValuesGameConfig;
    internal StartSpawnGame StartSpawnGameManager => _startSpawnGameManager;

    #endregion


    private void Awake()
    {
        _instanceGame = this;
        _supportGameManager = new SupportGameManager();

        _startValuesGameConfig = _supportGameManager.ResourcesLoadGameManager.StartValuesConfig;
        _startSpawnGameManager = new StartSpawnGame(_supportGameManager, out _parentTransformScrips);
        _unityEvents = new UnityEvents(_supportGameManager.Builder);
        gameObject.transform.SetParent(_parentTransformScrips);
    }

    private void Start()
    {
        _photonManager = new PhotonGameManager(_parentTransformScrips);
        _eCSmanager = new ECSmanager(_photonManager);
        _photonManager.InitAfterECS(_eCSmanager);
    }


    private void Update()
    {
        _eCSmanager.Update();
    }

    private void OnDestroy() { }
}