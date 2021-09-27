using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.Manager.View.Menu;
using Assets.Scripts.ECS.System.Common;
using Assets.Scripts.ECS.System.Data.Common;
using Assets.Scripts.ECS.System.View.Menu;
using Leopotam.Ecs;
using System;
using UnityEngine.Advertisements;

namespace Assets.Scripts
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

        private ComSysManager _comSysManag;
        private MenuSystemManager _menuSysManag;
        private GameGeneralSysManager _gameGenSysManag;
        private GameMasterSystemManager _gameMasSysManag;
        private GameOtherSystemManager _gameOthSysmManag;

        private DateTime _lastTimeAd;

        #endregion


        public ECSManager()
        {
            _lastTimeAd = DateTime.Now;

            _comWorld = new EcsWorld();
            _allComSystems = new EcsSystems(_comWorld);

            _allComSystems.Add(new SpawnInitComSys());
            _comSysManag = new ComSysManager(_comWorld, _allComSystems);
            _allComSystems.Init();


            if (Advertisement.isSupported) Advertisement.Initialize("4097313", false);
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
                        var t = DateTime.Now - _lastTimeAd;

                        if (t.Minutes >= 1)
                        {
                            //PurchasingSettings.
                            if (Advertisement.IsReady())
                            {
                                Advertisement.Show();
                                _lastTimeAd = DateTime.Now;
                            }
                        }



                        _gameWorld.Destroy();

                        _gameGenSysManag.Dispose();
                        _gameGenSysManag = default;
                        _gameMasSysManag = default;
                        _gameOthSysmManag = default;
                        _allGameSystems.Destroy();
                    }

                    _menuWorld = new EcsWorld();
                    _allMenuSystems = new EcsSystems(_menuWorld);

                    _allMenuSystems.Add(new InitSpawnMenuSys());
                    _menuSysManag = new MenuSystemManager(_menuWorld, _allMenuSystems);
                    _allMenuSystems.Init();
                    break;

                case SceneTypes.Game:
                    if (_menuWorld != default)
                    {
                        _menuWorld.Destroy();

                        _menuSysManag = default;
                        _allMenuSystems.Destroy();
                    }

                    _gameWorld = new EcsWorld();
                    _allGameSystems = new EcsSystems(_gameWorld);

                    _allGameSystems.Add(new InitSpawnGameSys());
                    _gameGenSysManag = new GameGeneralSysManager(_gameWorld, _allGameSystems);
                    if (PhotonNetwork.isMasterClient)
                    {
                        _gameMasSysManag = new GameMasterSystemManager(_gameWorld, _allGameSystems);
                    }
                    else
                    {
                        _gameOthSysmManag = new GameOtherSystemManager(_gameWorld, _allGameSystems);
                    }

                    _allGameSystems.Init();
                    break;

                default:
                    throw new Exception();
            }
        }


        public void OwnUpdate(SceneTypes sceneType)
        {
            _comSysManag.RunUpdate();

            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    _menuSysManag.RunUpdate();
                    break;

                case SceneTypes.Game:
                    _gameGenSysManag.RunUpdate();

                    if (PhotonNetwork.isMasterClient) _gameMasSysManag.RunUpdate();
                    else _gameOthSysmManag.RunUpdate();
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}