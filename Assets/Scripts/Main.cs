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

        ActionC _runUpdate;
        ActionC _runFixedUpdate;


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
                    _runUpdate.Invoke();
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
                    _runFixedUpdate.Invoke();
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

                        var entViews = new EntitiesView(gameW, out var forData);
                        var uIEs = new EntitiesViewUI(gameW);
                        var ents = new EntitiesModel(forData, RpcS.NamesMethods);

                        new SystemViewUI(ref _runUpdate, ref _runFixedUpdate, resources, ents, uIEs, out var updateUI);
                        new SystemsView(ref _runUpdate, ref _runFixedUpdate, ents, entViews, out var updateView);
                        new Systems(ref _runUpdate, ref _runFixedUpdate, ents, updateUI, updateView, out var runAfterDoing);


                        new Events(updateView, updateUI, ents, uIEs);

                        EntityVPool.Photon<PhotonE>().AddComponent<RpcS>().GiveData(ents, updateView,  updateUI, runAfterDoing);
                        RpcS.SyncAllMaster();

                        break;
                    }
                default: throw new Exception();
            }
        }
    }
}