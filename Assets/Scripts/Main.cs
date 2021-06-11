using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

internal sealed class Main : MonoBehaviour
{
    #region Variables

    [SerializeField] private bool _isOfflineMode;
    [SerializeField] private TestTypes _testType;
    [SerializeField] private SceneTypes _sceneType;

    private bool _isStart = true;
    private static Main _instance;
    private Builder _builder;
    private Names _names;
    private ResourcesLoad _resourcesLoad;
    private PhotonManager _photonManager;
    private ECSmanager _eCSmanagerGame;
    private UnityEvents _unityEvents;
    private GameObject _parentGOs;
    private EventManager _eventManager;
    private SaverData _saverData;
    private SoundManager _soundManager;
    private CanvasManager _canvasManager;
    private CameraManager _cameraManager;

    #endregion


    #region Properties

    internal static Main Instance => _instance;

    internal bool IsOfflineMode => _isOfflineMode;
    internal TestTypes TestType => _testType;
    internal SceneTypes SceneType => _sceneType;

    internal bool IsMasterClient => PhotonNetwork.IsMasterClient;
    internal Player MasterClient => PhotonNetwork.MasterClient;
    internal Player LocalPlayer => PhotonNetwork.LocalPlayer;

    internal StartValuesGameConfig StartValuesGameConfig => _resourcesLoad.StartValuesGameConfig;
    internal Names Names => _names;
    internal ECSmanager ECSmanagerGame => _eCSmanagerGame;
    internal ResourcesLoad ResourcesLoad => _resourcesLoad;
    internal Builder Builder => _builder;
    internal GameObject ParentGOs => _parentGOs;
    internal PhotonManager PhotonGameManager => _photonManager;
    internal CanvasManager CanvasManager => _canvasManager;

    #endregion


    private void Start()
    {
        PhotonNetwork.PhotonServerSettings.StartInOfflineMode = _isOfflineMode;

        _instance = this;
        _builder = new Builder();
        _names = new Names();
        _unityEvents = new UnityEvents();
        _resourcesLoad = new ResourcesLoad();
        _saverData = new SaverData();
        _photonManager = new PhotonManager();

        _canvasManager = new CanvasManager(_resourcesLoad, _names);
        _soundManager = new SoundManager(_resourcesLoad, _builder, _saverData);
        _cameraManager = new CameraManager(_resourcesLoad);

        _eCSmanagerGame = new ECSmanager();
        _eCSmanagerGame.InitAndProcessInjectsSystems();
        _eventManager = new EventManager(_eCSmanagerGame.EntitiesGeneralManager, _photonManager.PhotonPunRPC);
        
        _photonManager.PhotonPunRPC.InitAfterECS(_eCSmanagerGame, _eCSmanagerGame.CellManager, _eCSmanagerGame.EconomyManager);


        ToggleScene(_sceneType);
    }

    private void Update()
    {
        switch (_sceneType)
        {
            case SceneTypes.Menu:
                _soundManager.SyncValues();
                break;

            case SceneTypes.Game:
                _eCSmanagerGame.OwnUpdate();
                _photonManager.SceneManager.OwnUpdate();
                break;

            default:
                break;
        }
    }

    internal void ToggleScene(SceneTypes sceneType)
    {
        _sceneType = sceneType;

        switch (_sceneType)
        {
            case SceneTypes.Menu:
                if (_isStart)
                {
                    _isStart = !_isStart;
                }
                else
                {
                    Destroy(_canvasManager.InGameZoneGO);
                    Destroy(_parentGOs);
                }
                _parentGOs = new GameObject(_names.IN_MENU_GAME_ZONE);

                _canvasManager.ToggleScene(_sceneType);
                _soundManager.ToggleScene(_sceneType, _canvasManager.InMenuZoneCanvasGO);
                _cameraManager.ToggleScene(_sceneType);

                _photonManager.SceneManager.InitMenu();

                break;


            case SceneTypes.Game:
                if (_isStart)
                {
                    _isStart = !_isStart;
                }
                else
                {
                    Destroy(_parentGOs);
                    Destroy(_canvasManager.InMenuZoneCanvasGO);
                }

                _parentGOs = new GameObject(_names.GAME);

                _canvasManager.ToggleScene(_sceneType);
                _cameraManager.ToggleScene(_sceneType);
                _eCSmanagerGame.SpawnAndFillEntities();
                _eventManager.FillEvents();

                _photonManager.PhotonPunRPC.RefreshAllToMaster();

                break;

            default:
                break;
        }
    }
}