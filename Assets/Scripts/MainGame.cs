using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

internal sealed class MainGame : MonoBehaviour
{
    #region Variables

    private static MainGame _instance;

    private ResourcesLoadGame _resourcesLoadManager;
    private Builder _builder;
    private Names _names;
    private ObjectPool _gameObjectPool;
    private StartSpawnGame _startSpawnGame;
    private PhotonGameManager _photonGameManager;
    private ECSmanager _eCSmanager;
    private UnityEvents _unityEvents;
    private CellWorker _cellWorker;

    #endregion


    #region Properties

    public static MainGame Instance => _instance;

    public ResourcesLoadGame ResourcesLoadGameManager => _resourcesLoadManager;
    internal Builder Builder => _builder;
    internal Names Names => _names;
    internal ObjectPool ObjectPool => _gameObjectPool;
    internal StartValuesGameConfig StartValuesGameConfig => _resourcesLoadManager.StartValuesGameConfig;
    internal PhotonGameManager PhotonGameManager => _photonGameManager;
    internal CellWorker CellWorker => _cellWorker;

    internal bool IsMasterClient => PhotonNetwork.IsMasterClient;
    internal Player MasterClient => PhotonNetwork.MasterClient;
    internal Player LocalPlayer => PhotonNetwork.LocalPlayer;

    #endregion



    private void Start()
    {
        #region Casting

        _instance = this;

        _builder = new Builder();
        _names = new Names();
        _resourcesLoadManager = new ResourcesLoadGame();
        _gameObjectPool = new ObjectPool();


        _startSpawnGame = new StartSpawnGame(this);

        _unityEvents = new UnityEvents(_builder);
        gameObject.transform.SetParent(_gameObjectPool.ParentScriptsGO.transform);

        _photonGameManager = new PhotonGameManager(_gameObjectPool.ParentScriptsGO.transform);

        _cellWorker = new CellWorker();

        #endregion


        #region Static

        _eCSmanager = new ECSmanager();
        _cellWorker.InitAfterECS(_eCSmanager.EntitiesGeneralManager);
        _photonGameManager.PhotonPunRPC.InitAfterECS(_eCSmanager);

        #endregion
    }


    private void Update()
    {
        _eCSmanager.OwnUpdate();
        _photonGameManager.GameSceneManager.OwnUpdate();
    }

    private void OnDestroy() { }
}