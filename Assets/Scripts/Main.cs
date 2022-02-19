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
        SystemViewUI _systemViewUI;

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
                    _systems.Run(SystemDataTypes.RunUpdate);
                    _systemsV.Run(SystemViewDataTypes.RunUpdate);
                    _systemViewUI.Run(UITypes.RunUpdate);
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
                    _systems.Run(SystemDataTypes.RunFixedUpdate);
                    _systemsV.Run(SystemViewDataTypes.RunFixedUpdate);
                    _systemViewUI.Run(UITypes.RunFixedUpdate);
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
                        ref var gameW = ref _toggleW;

                        if (gameW != default) gameW = default;

                        gameW = new EcsWorld();

                        var resources = new Game.Resources(gameW);

                        var viewEs = new EntitiesView(gameW, out var forData);
                        var uIEs = new EntitiesViewUI(gameW);
                        var ents = new Entities(forData, RpcS.NamesMethods);


                        _systemViewUI = new SystemViewUI(resources, ents, uIEs, out var updateUI);
                        _systemsV = new SystemsView(ents, viewEs, out var updateView);

                        new Events(updateView, updateUI, ents, uIEs);

                        _systems = new Systems(ents, updateView, updateUI);

                        EntityVPool.Photon<PhotonVC>().AddComponent<RpcS>().GiveData(ents, _systems, updateView,  updateUI);
                        RpcS.SyncAllMaster();

                        break;
                    }
                default: throw new Exception();
            }
        }
    }
}