using ECS;
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
        EcsWorld _toggleW;

        #region Game

        Systems _systems;
        SystemsView _systemsV;

        #endregion


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
                    _systems.Run(DataSTypes.RunUpdate);
                    _systemsV.Run(SystemViewDataTypes.RunUpdate);
                    _systemsV.SystemViewUI.Run(UITypes.RunUpdate);
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
                    _systems.Run(DataSTypes.RunFixedUpdate);
                    _systemsV.Run(SystemViewDataTypes.RunFixedUpdate);
                    _systemsV.SystemViewUI.Run(UITypes.RunFixedUpdate);
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
                        if (_toggleW != default) _toggleW = default;

                        _toggleW = new EcsWorld();
                        new EntitieManager(_toggleW);
                        new SystemsManager(default);
                        break;
                    }

                case SceneTypes.Game:
                    {
                        if (_toggleW != default) _toggleW = default;

                        _toggleW = new EcsWorld();

                        var entsView = new EntitiesView(_toggleW, out var forData);
                        var ents = new Entities(_toggleW, forData, RpcS.NamesMethods);

                        new FillCellsS(ents);

                        new Events(ents, entsView);
                        _systemsV = new SystemsView(ents, entsView);
                        _systems = new Systems(ents, _systemsV);

                        EntityVPool.Photon<PhotonVC>().AddComponent<RpcS>().GiveData(ents, _systems);
                        RpcS.SyncAllMaster();

                        break;
                    }
                default: throw new Exception();
            }
        }
    }
}