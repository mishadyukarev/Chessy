﻿using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

internal sealed class Main : MonoBehaviour
{
    #region Variables

    [SerializeField] private bool _isOfflineMode;
    [SerializeField] private TestTypes _testType;
    [SerializeField] private SceneTypes _sceneType;

    private List<IDisposable> _menuDisposables;
    private List<IDisposable> _gameDisposables;
    private bool _isStart = true;
    private Camera _camera;
    private static Main _instance;
    private Builder _builder;
    private Names _names;
    private ResourcesLoad _resourcesLoad;
    private Canvas _canvas;
    private PhotonManager _photonManager;
    private ECSmanagerGame _eCSmanagerGame;
    private UnityEvents _unityEvents;
    private CellManager _cellManager;
    protected EconomyManager _economyManager;
    private ObjectPoolGame _objectPoolGame;
    private ObjectPoolMenu _objectPoolMenu;
    private CanvasGameManager _canvasGameManager;
    private CanvasMenuManager _canvasMenuManager;

    #endregion


    #region Properties

    internal static Main Instance => _instance;

    internal bool IsOfflineMode => _isOfflineMode;
    internal TestTypes TestType => _testType;
    internal SceneTypes SceneType => _sceneType;

    internal bool IsMasterClient => PhotonNetwork.IsMasterClient;
    internal Player MasterClient => PhotonNetwork.MasterClient;
    internal Player LocalPlayer => PhotonNetwork.LocalPlayer;

    internal List<IDisposable> MenuDisposables => _menuDisposables;
    internal List<IDisposable> GameDisposables => _gameDisposables;
    internal StartValuesGameConfig StartValuesGameConfig => _resourcesLoad.StartValuesGameConfig;
    internal Names Names => _names;
    internal CellManager CellManager => _cellManager;
    internal EconomyManager EconomyManager => _economyManager;
    internal ECSmanagerGame ECSmanagerGame => _eCSmanagerGame;
    internal CanvasGameManager CanvasGameManager => _canvasGameManager;
    internal ObjectPoolGame ObjectPoolGame => _objectPoolGame;

    internal PhotonManager PhotonGameManager => _photonManager;

    #endregion

    private void Start()
    {
        _instance = this;

        PhotonNetwork.PhotonServerSettings.StartInOfflineMode = _isOfflineMode;

        _builder = new Builder();
        _names = new Names();
        _unityEvents = new UnityEvents(_builder);
        _resourcesLoad = new ResourcesLoad();
        _canvas = GameObject.Instantiate(_resourcesLoad.Canvas);

        _canvasMenuManager = new CanvasMenuManager(_canvas);
        _canvasGameManager = new CanvasGameManager(_canvas);

        _camera = GameObject.Instantiate(_resourcesLoad.Camera);
        _camera.gameObject.transform.position += new Vector3(7, 5.5f, -2);

        _photonManager = new PhotonManager(gameObject, _canvasMenuManager);

        _cellManager = new CellManager();
        _economyManager = new EconomyManager();

        _objectPoolMenu = new ObjectPoolMenu();
        _objectPoolGame = new ObjectPoolGame();

        _menuDisposables = new List<IDisposable>();
        _gameDisposables = new List<IDisposable>();

        _eCSmanagerGame = new ECSmanagerGame(_canvasGameManager, _resourcesLoad.StartValuesGameConfig, _cellManager, _names);
        _eCSmanagerGame.InitAndProcessInjectsSystems();
        _cellManager.InitAfterECS(_eCSmanagerGame);
        _economyManager.InitAfterECS(_eCSmanagerGame);
        _photonManager.PhotonPunRPC.InitAfterECS(_eCSmanagerGame, _cellManager, _economyManager);

        ToggleScene(_sceneType);
    }

    private void Update()
    {
        switch (_sceneType)
        {
            case SceneTypes.Menu:
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
                if (_isStart) _isStart = false;
                else foreach (var disposed in _gameDisposables) disposed.Dispose();

                _objectPoolMenu.Spawn(_resourcesLoad, _builder);
                _canvasMenuManager.Active(true);
                _canvasGameManager.Active(false);
                break;


            case SceneTypes.Game:
                foreach (var disposed in _menuDisposables) disposed.Dispose();

                _objectPoolGame.Spawn(_resourcesLoad, _builder);
                _canvasMenuManager.Active(false);
                _canvasGameManager.Active(true);

                _camera.transform.rotation = IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);
                _eCSmanagerGame.FillEntities(_objectPoolGame, _canvasGameManager, _resourcesLoad.StartValuesGameConfig);
                if (!IsMasterClient) _photonManager.PhotonPunRPC.RefreshAllToMaster();

                break;

            default:
                break;
        }
    }
}