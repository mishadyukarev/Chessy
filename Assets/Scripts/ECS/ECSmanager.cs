using Assets.Scripts.ECS.Manager.View.Menu;
using Assets.Scripts.ECS.System.Common;
using Assets.Scripts.ECS.Systems.UI.Game.General.UI;
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

        private GameGeneralSystemManager _gameGeneralSystemManager;
        private GameGeneralUISystemManager _gameGeneralUISystemManager;
        private GameMasterSystemManager _gameMasterSystemManager;
        private GameOtherSystemManager _gameOtherSystemManager;


        public ECSManager()
        {
            _commonWorld = new EcsWorld();

            _commonSystemManager = new CommonSystemManager(_commonWorld);
            _commonSystemManager.Init();
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
                        _gameGeneralUISystemManager = default;
                        _gameMasterSystemManager = default;
                        _gameOtherSystemManager = default;
                    }

                    _menuWorld = new EcsWorld();
                    _menuSystemManager = new MenuSystemManager(_menuWorld);
                    _menuSystemManager.Init();
                    break;

                case SceneTypes.Game:
                    if (_menuWorld != default)
                    {
                        _menuWorld.Destroy();
                        _menuSystemManager = default;
                    }

                    _gameWorld = new EcsWorld();
                    _gameGeneralSystemManager = new GameGeneralSystemManager(_gameWorld);
                    _gameGeneralUISystemManager = new GameGeneralUISystemManager(_gameWorld);
                    _gameMasterSystemManager = new GameMasterSystemManager(_gameWorld);
                    _gameOtherSystemManager = new GameOtherSystemManager(_gameWorld);
                    _gameGeneralSystemManager.Init();
                    _gameGeneralUISystemManager.Init();
                    _gameMasterSystemManager.Init();
                    _gameOtherSystemManager.Init();

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
                    _gameGeneralUISystemManager.RunUpdate();

                    if (PhotonNetwork.IsMasterClient) _gameMasterSystemManager.RunUpdate();
                    else _gameOtherSystemManager.RunUpdate();
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}