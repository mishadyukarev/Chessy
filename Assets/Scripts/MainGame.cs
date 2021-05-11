using UnityEngine;

internal sealed class MainGame : Main
{

    #region Variables

    private static MainGame _instanceGame;

    private ResourcesLoadGame _resourcesLoadManager;
    private Builder _builder;
    private Names _names;
    private CellManager _cellManager;
    private StartValuesGameConfig _startValuesGameConfig;
    private GameObjectPool _gameObjectPool;
    private StartSpawnGame _startSpawnGame;
    private PhotonGameManager _photonGameManager;
    private ECSmanager _eCSmanager;

    #endregion


    #region Properties

    public static MainGame InstanceGame => _instanceGame;

    public ResourcesLoadGame ResourcesLoadGameManager => _resourcesLoadManager;
    internal Builder Builder => _builder;
    internal Names Names => _names;
    internal CellManager CellManager => _cellManager;
    internal GameObjectPool GameObjectPool => _gameObjectPool;
    internal StartValuesGameConfig StartValuesGameConfig => _resourcesLoadManager.StartValuesConfig;
    internal PhotonGameManager PhotonGameManager => _photonGameManager;

    #endregion



    private void Start()
    {
        _instanceGame = this;

        _builder = new Builder();
        _names = new Names();
        _resourcesLoadManager = new ResourcesLoadGame();
        _cellManager = new CellManager();
        _gameObjectPool = new GameObjectPool();


       _startSpawnGame = new StartSpawnGame(_gameObjectPool, _resourcesLoadManager, _builder);

        _unityEvents = new UnityEvents(_builder);
        gameObject.transform.SetParent(_gameObjectPool.ParentScriptsGO.transform);

        _photonGameManager = new PhotonGameManager(_gameObjectPool.ParentScriptsGO.transform);


        _eCSmanager = new ECSmanager();
        _photonGameManager.PhotonPunRPC.InitAfterECS(_eCSmanager);
        _cellManager.CellFinderWay.InitAfterECS(_eCSmanager);
    }


    private void Update()
    {
        _eCSmanager.Update();
    }

    private void OnDestroy() { }
}