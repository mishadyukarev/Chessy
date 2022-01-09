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

        WorldEcs _commonW;
        WorldEcs _menuW;
        WorldEcs _gameW;


        void Start()
        {
            _commonW = new WorldEcs();

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
                    Game.DataSC.Run(DataSTypes.RunUpdate);
                    DataMastSC.RunUpdate();
                    ViewDataSC.Run(ViewDataSTypes.RunUpdate);
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
                    Menu.DataSC.RunUpdate();
                    break;

                case SceneTypes.Game:
                    Game.DataSC.Run(DataSTypes.RunFixedUpdate);
                    ViewDataSC.Run(ViewDataSTypes.RunFixedUpdate);
                    break;

                default:
                    throw new Exception();
            }
        }

        void ToggleScene(SceneTypes newScene)
        {
            if (CurSceneC.Is(newScene)) throw new Exception("Need other scene");

            CurSceneC.Set(newScene);
            switch (newScene)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    if (_gameW != default) _gameW = default;

                    _menuW = new WorldEcs();
                    new EntitieManager(_menuW);
                    new SystemsManager();
                    break;

                case SceneTypes.Game:
                    if (_menuW != default) _menuW = default;

                    _gameW = new WorldEcs();

                    new SpawnEntities(_gameW);

                    new SystemDataManager();
                    new SystemDataMasterManager();
                    new SystemViewDataManager();


                    var rpc_GO = new GameObject("RpcView");
                    var rpc = rpc_GO.AddComponent<RpcS>();
                    EntityVPool.GeneralZone<GeneralZoneVEC>().Attach(rpc.transform);
                    new RpcVC(rpc_GO);

                    rpc.Init();

                    break;

                default: throw new Exception();
            }
        }
    }
}