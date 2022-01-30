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
        EcsWorld _menuW;
        EcsWorld _gameW;

        Systems _systems;


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
                    SystemsView.Run(ViewDataSystemTypes.RunUpdate);
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
                    _systems.Run(DataSTypes.RunFixedUpdate);
                    SystemsView.Run(ViewDataSystemTypes.RunFixedUpdate);
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

                        var entsView = new EntitiesView(_gameW, out var forData);
                        var ents = new Entities(_gameW, forData, RpcS.NamesMethods);

                        new FillCellsS(ents);

                        new Events(ents, entsView);
                        _systems = new Systems(ents);
                        new SystemsView(ents, entsView);

                        EntityVPool.Photon<PhotonVC>().AddComponent<RpcS>().GiveData(ents, _systems);
                        RpcS.SyncAllMaster();

                        break;
                    }
                default: throw new Exception();
            }
        }
    }
}