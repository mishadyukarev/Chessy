using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Component.Common;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.Manager.View.Menu;
using Assets.Scripts.ECS.System.Common;
using Assets.Scripts.ECS.System.View.Menu;
using Leopotam.Ecs;
using Photon.Pun;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class ECSManager
    {
        #region 

        private EcsWorld _commonWorld;
        private EcsWorld _menuWorld;
        private EcsWorld _gameWorld;

        private EcsSystems _allComSystems;
        private EcsSystems _allMenuSystems;
        private EcsSystems _allGameSystems;

        private ComSysManager _commonSystemManager;
        private MenuSystemManager _menuSystemManager;
        private GameGeneralSystemManager _gameGeneralSystemManager;
        private GameMasterSystemManager _gameMasterSystemManager;
        private GameOtherSystemManager _gameOtherSystemManager;

        private PhotonSceneSys _photonSceneSys;
        private RpcGeneralSystem _rpcGameSys;

        #endregion


        public ECSManager()
        {
            _photonSceneSys = Main.Instance.gameObject.AddComponent<PhotonSceneSys>();

            _commonWorld = new EcsWorld();
            _allComSystems = new EcsSystems(_commonWorld)
                .Add(_photonSceneSys);

            _commonSystemManager = new ComSysManager(_commonWorld, _allComSystems);
            _allComSystems.Init();
        }

        public void ToggleScene(SceneTypes sceneType)
        {
            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    if (_gameWorld != default)
                    {
                        _gameWorld.Destroy();

                        if (PhotonViewComponent.PhotonView.gameObject != default)
                            GameObject.Destroy(PhotonViewComponent.PhotonView.gameObject);

                        GameObject.Destroy(_rpcGameSys);
                        _gameGeneralSystemManager = default;
                        _gameMasterSystemManager = default;
                        _gameOtherSystemManager = default;
                        _allGameSystems.Destroy();
                    }

                    _menuWorld = new EcsWorld();
                    _allMenuSystems = new EcsSystems(_menuWorld);

                    _allMenuSystems.Add(new SpawnMenuSys());
                    _menuSystemManager = new MenuSystemManager(_menuWorld, _allMenuSystems);
                    _allMenuSystems.Init();
                    break;

                case SceneTypes.Game:
                    if (_menuWorld != default)
                    { 
                        _menuWorld.Destroy();

                        _menuSystemManager = default;
                        _allMenuSystems.Destroy();
                    }

                    


                    _gameWorld = new EcsWorld();
                    _allGameSystems = new EcsSystems(_gameWorld);


                    var go = new GameObject("Ph");
                    PhotonViewComponent.PhotonView = go.AddComponent<PhotonView>();
                    PhotonViewComponent.PhotonView.ViewID = 1001;
                    _rpcGameSys = go.AddComponent<RpcGeneralSystem>();

                    _allGameSystems
                        .Add(new SpawnGameSys())
                        .Add(_rpcGameSys);

                    _gameGeneralSystemManager = new GameGeneralSystemManager(_gameWorld, _allGameSystems);
                    if (PhotonNetwork.IsMasterClient)
                    {
                        _gameMasterSystemManager = new GameMasterSystemManager(_gameWorld, _allGameSystems);

                        
                    }
                    else
                    {
                        _gameOtherSystemManager = new GameOtherSystemManager(_gameWorld, _allGameSystems);
                    }

                    

                    _allGameSystems.Init();             
                    break;

                default:
                    throw new Exception();
            }

            _photonSceneSys.ToggleScene(sceneType, _menuSystemManager);
        }


        public void OwnUpdate(SceneTypes sceneType)
        {
            Debug.Log(PhotonNetwork.InRoom);


            _commonSystemManager.RunUpdate();

            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    _menuSystemManager.RunUpdate();
                    break;

                case SceneTypes.Game:
                    _gameGeneralSystemManager.RunUpdate();

                    if (PhotonNetwork.IsMasterClient) _gameMasterSystemManager.RunUpdate();
                    else _gameOtherSystemManager.RunUpdate();
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}