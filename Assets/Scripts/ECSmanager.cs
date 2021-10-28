using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;
using Scripts.Game;
using Scripts.Menu;
using System;
using UnityEngine;

namespace Scripts
{
    public sealed class ECSManager
    {
        #region 

        private EcsWorld _comWorld;
        private EcsWorld _menuWorld;
        private EcsWorld _gameWorld;

        private EcsSystems _allComSystems;
        private EcsSystems _allMenuSystems;
        private EcsSystems _allGameSystems;

        private ComSysDataM _comSysDataM;
        private MenuSystemManager _menuSysManag;
        private GameGenSysDataM _gameGenSysDataM;
        private GameGenSysViewM _gameGenSysViewM;
        private GameMasSysDataM _gameMasSysDataM;
        private GameOthSysDataM _gameOthSysDataM;

        #endregion


        public ECSManager(Action<SceneTypes> toggleScene_Action, GameObject main_GO)
        {
            _comWorld = new EcsWorld();
            _allComSystems = new EcsSystems(_comWorld);

            _allComSystems.Add(new SpawnInitComSys(toggleScene_Action, main_GO));
            _comSysDataM = new ComSysDataM(_comWorld, _allComSystems);
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

                        _gameGenSysDataM.Dispose();
                        _gameGenSysDataM = default;
                        _gameMasSysDataM = default;
                        _gameOthSysDataM = default;
                        _allGameSystems.Destroy();
                    }

                    _menuWorld = new EcsWorld();
                    _allMenuSystems = new EcsSystems(_menuWorld);

                    _allMenuSystems.Add(new Menu.InitSpawnSys());
                    _menuSysManag = new MenuSystemManager(_menuWorld, _allMenuSystems);
                    _allMenuSystems.Init();


                    _comSysDataM.LaunchAdSys.Run();
                    _menuSysManag.LaunchLikeGameSys.Run();
                    break;

                case SceneTypes.Game:
                    if (_menuWorld != default)
                    {
                        _menuWorld.Destroy();

                        _menuSysManag = default;
                        _allMenuSystems.Destroy();
                    }

                    _gameWorld = new EcsWorld();
                    _allGameSystems = new EcsSystems(_gameWorld)
                        .Add(new Game.InitSpawnSys());

                    _gameGenSysDataM = new GameGenSysDataM(_gameWorld, _allGameSystems);
                    _gameMasSysDataM = new GameMasSysDataM(_gameWorld, _allGameSystems);
                    _gameOthSysDataM = new GameOthSysDataM(_gameWorld, _allGameSystems);

                    _gameGenSysViewM = new GameGenSysViewM(_gameWorld, _allGameSystems);

                    _allGameSystems.Init();
                    break;

                default:
                    throw new Exception();
            }
        }


        public void OwnUpdate(SceneTypes sceneType)
        {
            _comSysDataM.RunUpdate();

            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    _menuSysManag.RunUpdate();
                    break;

                case SceneTypes.Game:
                    _gameGenSysDataM.RunUpdate();
                    _gameGenSysViewM.RunUpdate();

                    if (PhotonNetwork.IsMasterClient) _gameMasSysDataM.RunUpdate();
                    else _gameOthSysDataM.RunUpdate();
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}