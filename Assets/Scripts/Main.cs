﻿using ECS;
using Game.Common;
using Game.Game;
using Game.Menu;
using System;
using UnityEngine;

namespace Game
{
    sealed class Main : MonoBehaviour
    {
        [SerializeField] TestModes _testMode = default;

        EcsWorld _commonW;
        EcsWorld _menuW;
        EcsWorld _gameW;


        void Start()
        {
            _commonW = new EcsWorld();

            new Common.CreateCs(transform, _testMode);


            new Common.CreateSs(ToggleScene);
            new Common.CreateVSs(gameObject);

            ToggleScene(SceneTypes.Menu);
        }

        void Update()
        {
            switch (CurSceneC.Scene)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    break;

                case SceneTypes.Game:
                    SystemDataManager.Run(DataSTypes.RunUpdate);
                    SystemViewDataManager.Run(ViewDataSystemTypes.RunUpdate);
                    SystemViewDataUIManager.Run(UITypes.RunUpdate);
                    break;

                default:
                    throw new Exception();
            }
        }

        void FixedUpdate()
        {
            Common.DataSC.RunUpdate();

            switch (CurSceneC.Scene)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    SystemsManager.RunUpdate();
                    break;

                case SceneTypes.Game:
                    SystemDataManager.Run(DataSTypes.RunFixedUpdate);
                    SystemViewDataManager.Run(ViewDataSystemTypes.RunFixedUpdate);
                    SystemViewDataUIManager.Run(UITypes.RunFixedUpdate);
                    break;

                default:
                    throw new Exception();
            }
        }

        void ToggleScene(SceneTypes newScene)
        {
            if (CurSceneC.Is(newScene)) throw new Exception("Need other scene");

            CurSceneC.Scene = newScene;
            switch (newScene)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    {
                        if (_gameW != default) _gameW = default;

                        _menuW = new EcsWorld();
                        new EntitieManager(_menuW);
                        new SystemsManager(default);
                        break;
                    }

                case SceneTypes.Game:
                    {
                        if (_menuW != default) _menuW = default;

                        _gameW = new EcsWorld();


                        #region Entities

                        #region View

                        #region Resources

                        new ResourceSpriteVEs(_gameW);
                        new VideoClipsResC(true);

                        #endregion

                        new EntitiesVPool(_gameW, out var forData);

                        new RightUIEntities(_gameW);
                        new EventUIManager(default);

                        #endregion


                        #region Data

                        new Entities(_gameW, forData, RpcS.NamesMethods, out var i);
                        new EntitiesMaster(_gameW);

                        var isActiveParenCells = (bool[])forData[i++];
                        var idCells = (int[])forData[i++];

                        new CellEs(_gameW, isActiveParenCells, idCells);
                        new CellUnitEs(_gameW);
                        new CellBuildEs(_gameW);
                        new CellTrailEs(_gameW);
                        new CellEnvironmentEs(_gameW);
                        new CellFireEs(_gameW);
                        new CellRiverEs(_gameW);

                        new AvailableCenterUpgradeEs(_gameW);

                        #endregion

                        #endregion


                        new FillCellsS();
                        EntityVPool.Photon<PhotonVC>().AddComponent<RpcS>();


                        #region Systems

                        new SystemDataManager(default);
                        new SystemDataMasterManager(default);
                        new SystemDataOtherManager(default);

                        new SystemViewDataManager(default);
                        new SystemViewDataUIManager(default);

                        RpcS.SyncAllMaster();

                        #endregion

                        break;
                    }
                default: throw new Exception();
            }
        }
    }
}