using Chessy.Common;
using Chessy.Game;
using Chessy.Game.EventsUI;
using Chessy.Game.System.View.UI;
using Chessy.Menu;
using ECS;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Chessy
{
    sealed class Main : MonoBehaviour
    {
        [SerializeField] TestModes _testMode = default;

        EcsWorld _toggleW;

        EntitiesModel _e;
        EntitiesView _eV;
        EntitiesViewUI _eUI;

        float _timer;

        void Start()
        {
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
                    SystemModelUpdate.Run(ref _e);

                    _timer += Time.deltaTime;   
                    if (_timer >= 0.04f)
                    {
                        SystemViewUpdate.Run(_eV, _e);
                        SystemViewUIUpdate.Run(_timer, _eUI, _e);
                        _timer = 0;
                    }
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
                        }

                        _toggleW = new EcsWorld();
                        new EntitieManager(_toggleW);
                        new SystemsManager(default);
                        break;
                    }

                case SceneTypes.Game:
                    {
                        _eV = new EntitiesView(out var forData);
                        _e = new EntitiesModel(forData, Rpc.NamesMethods);
                        _eUI = new EntitiesViewUI(_e);

                        var eventsUI = new EventsUIManager(_eUI, _e);

                        _eV.EntityVPool.Photon.AddComponent<Rpc>().GiveData(_e, eventsUI);
                        Rpc.SyncAllMaster();

                        break;
                    }
                default: throw new Exception();
            }
        }
    }
}