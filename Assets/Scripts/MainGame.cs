using UnityEngine;

internal sealed class MainGame : Main
{

    #region Variables

    internal readonly bool IS_TEST = true;

    private static MainGame _instanceGame;

    private Transform _parentTransformScrips;
    private PhotonGameManager _photonManager;
    private ECSmanager _eCSmanager;
    private SupportGameManager _supportGameManager;

    #endregion


    #region Properties

    public static MainGame InstanceGame => _instanceGame;

    internal StartValuesGameConfig StartValuesGameConfig => _supportGameManager.StartValuesGameConfig;
    internal StartSpawnGameManager StartSpawnGameManager => _supportGameManager.StartSpawnGameManager;
    internal CellManager CellManager => _supportGameManager.CellManager;
    internal BuilderManager BuilderManager => _supportGameManager.BuilderManager;
    internal NameManager NameManager => _supportGameManager.NameManager;

    #endregion


    private void Awake()
    {
        _instanceGame = this;
        _supportGameManager = new SupportGameManager(out _parentTransformScrips);
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

    private void FixedUpdate()
    {
        _eCSmanager.FixedUpdate();
    }

    private void OnDestroy() { }
}