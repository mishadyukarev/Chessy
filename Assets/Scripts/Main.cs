using ECS;
using Chessy.Common;
using Chessy.Game;
using Chessy.Menu;
using System;
using UnityEngine;

namespace Chessy
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
                        if (_toggleW != default)
                        {
                            _toggleW = default;
                            _runUpdate.Action = default;
                            _runFixedUpdate.Action = default;
                        }

                        _toggleW = new EcsWorld();
                        new EntitieManager(_toggleW);
                        new SystemsManager(default);
                        break;
                    }

                case SceneTypes.Game:
                    {
                        var resources = new Game.Resources(default);

                        var entViews = new EntitiesView(out var forData);
                        var uIEs = new EntitiesViewUI();
                        var ents = new EntitiesModel(forData, Rpc.NamesMethods);

                        new SystemViewUI(ref _runUpdate, ref _runFixedUpdate, resources, uIEs, ents,  out var updateUI);
                        new SystemsView(ref _runUpdate, ref _runFixedUpdate, ents, entViews, out var updateView);
                        new SystemsModel(ref _runUpdate, ents, updateUI, updateView, out var runAfterDoing);

                        var eventsUI = new EventsUI(updateView, updateUI, uIEs,  ents);

                        entViews.EntityVPool.Photon.AddComponent<Rpc>().GiveData(ents, updateView,  updateUI, runAfterDoing, eventsUI);
                        Rpc.SyncAllMaster();

                        break;
                    }
                default: throw new Exception();
            }
        }
    }
}