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

        private EcsWorld _menuWorld;
        private EcsWorld _gameWorld;

        private EcsSystems _allComSystems;
        //private EcsSystems _allGameSystems;

        private GameGenSysViewM _gameGenSysViewM;
        //private GameMasSysDataM _gameMasSysDataM;
        private GameOthSysDataM _gameOthSysDataM;

        #endregion


        public ECSManager(Action<SceneTypes> toggleScene, GameObject main_GO)
        {
            _allComSystems = new EcsSystems(new EcsWorld());
            new SpawnInitComSys(_allComSystems, toggleScene, main_GO);
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
                        FillEntitiesSys.Dispose();
                    }

                    _menuWorld = new EcsWorld();
                    new Menu.InitSpawnSys(new EcsSystems(_menuWorld));
                    break;

                case SceneTypes.Game:
                    if (_menuWorld != default)
                    {
                        _menuWorld.Destroy();
                    }

                    _gameWorld = new EcsWorld();
                    new Game.FillEntitiesSys(new EcsSystems(_gameWorld));

                    //_gameOthSysDataM = new GameOthSysDataM(_gameWorld, systs);

                    //_gameGenSysViewM = new GameGenSysViewM(_gameWorld, systs);

                    //systs.Init();

                    //GenViewSysC.RotateAll.Invoke();
                    break;

                default:
                    throw new Exception();
            }
        }


        public void OwnUpdate(SceneTypes sceneType)
        {
            //_comSysDataM.RunUpdate();

            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    Menu.InitSpawnSys.Run.Run();
                    //_menuSysManag.RunUpdate();
                    break;

                case SceneTypes.Game:
                    FillEntitiesSys.Run.Run();
                    //_gameGenSysViewM.RunUpdate();

                    //if (PhotonNetwork.IsMasterClient) _gameMasSysDataM.RunUpdate();
                    //else _gameOthSysDataM.RunUpdate();
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}