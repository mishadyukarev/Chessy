using Assets.Scripts.ECS.Manager.View.Menu;
using Assets.Scripts.ECS.System.Common;
using Leopotam.Ecs;
using Photon.Pun;
using System;

namespace Assets.Scripts
{
    public sealed class ECSManager
    {
        #region 

        private EcsWorld _commonWorld;
        private EcsWorld _menuWorld;
        private EcsWorld _gameWorld;

        private EcsSystems _allCommSystems;
        private CommonSystemManager _commonSystemManager;

        private EcsSystems _allMenuSystems;
        private MenuSystemManager _menuSystemManager;

        private EcsSystems _allGameSystems;
        private GameGeneralSystemManager _gameGeneralSystemManager;
        private GameMasterSystemManager _gameMasterSystemManager;
        private GameOtherSystemManager _gameOtherSystemManager;

        #endregion


        public ECSManager()
        {
            _commonWorld = new EcsWorld();
            _allCommSystems = new EcsSystems(_commonWorld);

            _commonSystemManager = new CommonSystemManager(_commonWorld, _allCommSystems);
            _allCommSystems.Init();
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
                        _gameGeneralSystemManager.Dispose();
                        _gameGeneralSystemManager = default;
                        _gameMasterSystemManager = default;
                        _gameOtherSystemManager = default;
                        _allGameSystems.Destroy();
                    }

                    _menuWorld = new EcsWorld();
                    _allMenuSystems = new EcsSystems(_menuWorld);

                    _menuSystemManager = new MenuSystemManager(_menuWorld, _allMenuSystems);
                    _allMenuSystems.Init();
                    break;

                case SceneTypes.Game:
                    if (_menuWorld != default)
                    { 
                        _menuWorld.Destroy();
                        _menuSystemManager.Dispose();
                        _menuSystemManager = default;
                        _allMenuSystems.Destroy();
                    }

                    _gameWorld = new EcsWorld();
                    _allGameSystems = new EcsSystems(_gameWorld);

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
        }


        public void OwnUpdate(SceneTypes sceneType)
        {
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