﻿using Assets.Scripts.ECS.Manager.View.Menu;
using Assets.Scripts.ECS.System.Common;
using Assets.Scripts.ECS.System.Data.Common;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using Photon.Pun;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class ECSManager
    {
        private EcsWorld _commonWorld;
        private EcsWorld _menuWorld;
        private EcsWorld _gameWorld;

        private CommonSystemManager _commonSystemManager;

        private MenuSystemManager _menuSystemManager;

        public GameGeneralSystemManager GameGeneralSystemManager { get; private set; }
        public GameMasterSystemManager GameMasterSystemManager { get; private set; }
        public GameOtherSystemManager GameOtherSystemManager { get; private set; }


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
                        GameGeneralSystemManager = default;
                        GameMasterSystemManager = default;
                        GameOtherSystemManager = default;
                    }

                    _menuWorld = new EcsWorld();
                    _menuSystemManager = new MenuSystemManager(_menuWorld, _commonWorld);
                    _menuSystemManager.Init();
                    break;

                case SceneTypes.Game:
                    if (_menuWorld != default)
                    {
                        _menuWorld.Destroy();
                        _menuSystemManager = default;
                    }

                    _gameWorld = new EcsWorld();
                    GameGeneralSystemManager = new GameGeneralSystemManager(_gameWorld, _commonWorld);
                    GameMasterSystemManager = new GameMasterSystemManager(_gameWorld);
                    GameOtherSystemManager = new GameOtherSystemManager(_gameWorld);
                    GameGeneralSystemManager.Init();
                    GameMasterSystemManager.Init();
                    GameOtherSystemManager.Init();


                    if (PhotonNetwork.IsMasterClient)
                    {
                        if (MainCommonSystem.CommonEnt_SaverCom.StepModeType == StepModeTypes.ByQueue)
                        {
                            DownDonerUIDataContainer.SetDoned(false, true);
                        }
                    }


                    if (PhotonNetwork.IsMasterClient)
                    {
                        MainCommonSystem.CommonEnt_CameraCom.Camera.transform.rotation = new Quaternion(0, 0, 0, 0);
                        MainCommonSystem.CommonEnt_CameraCom.Camera.transform.position = Main.Instance.transform.position + MainCommonSystem.CommonEnt_CameraCom.PosForCamera;
                    }
                    else
                    {
                        MainCommonSystem.CommonEnt_CameraCom.Camera.transform.rotation = new Quaternion(0, 0, 180, 0);
                        MainCommonSystem.CommonEnt_CameraCom.Camera.transform.position = Main.Instance.transform.position + MainCommonSystem.CommonEnt_CameraCom.PosForCamera + new Vector3(0, 0.5f, 0);
                    }

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
                    GameGeneralSystemManager.RunUpdate();

                    if (PhotonNetwork.IsMasterClient) GameMasterSystemManager.RunUpdate();
                    else GameOtherSystemManager.RunUpdate();
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}