using Assets.Scripts.ECS.Manager.View.Menu;
using Assets.Scripts.ECS.System.Common;
using Leopotam.Ecs;
using Photon.Pun;
using System;

namespace Assets.Scripts
{
    public sealed class ECSManager
    {
        private EcsWorld _commonWorld;
        private EcsWorld _menuWorld;
        private EcsWorld _gameWorld;


        private CommonSystemManager _commonSystemManager;

        private MenuSystemManager _menuSystemManager;

        private EcsSystems _allGameSystems;
        private GameGeneralSystemManager _gameGeneralSystemManager;
        private GameMasterSystemManager _gameMasterSystemManager;
        private GameOtherSystemManager _gameOtherSystemManager;


        public ECSManager()
        {
            _commonWorld = new EcsWorld();

            _commonSystemManager = new CommonSystemManager(_commonWorld);
            _commonSystemManager.AllSystems.Init();
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
                        _gameGeneralSystemManager = default;
                        _gameMasterSystemManager = default;
                        _gameOtherSystemManager = default;
                    }

                    _menuWorld = new EcsWorld();
                    _menuSystemManager = new MenuSystemManager(_menuWorld);
                    _menuSystemManager.AllSystems.Init();
                    break;

                case SceneTypes.Game:
                    if (_menuWorld != default)
                    {
                        _menuWorld.Destroy();
                        _menuSystemManager = default;
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