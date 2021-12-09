using Game.Common;
using Game.Game;
using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Game
{
    public sealed class Main : MonoBehaviour
    {
        [SerializeField] TestModes _testMode = default;

        EcsWorld _menuW;
        EcsWorld _gameW;


        void Start()
        {
            new Common.CreateCs(transform, _testMode);

            var comSysts = new EcsSystems(new EcsWorld());
            new Common.CreateVSs(comSysts, gameObject);
            new Common.CreateSs(comSysts, ToggleScene);

            comSysts.Init();

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
                    Game.DataSC.Run(DataSystTypes.RunUpdate);
                    DataMastSC.RunUpdate();
                    ViewDataSC.Run(ViewDataSTypes.RunUpdate);
                    break;

                default:
                    throw new Exception();
            }
        }

        private void FixedUpdate()
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
                    Game.DataSC.Run(DataSystTypes.RunFixedUpdate);
                    ViewDataSC.Run(ViewDataSTypes.RunFixedUpdate);
                    break;

                default:
                    throw new Exception();
            }
        }

        private void ToggleScene(SceneTypes scene)
        {
            CurSceneC.Set(scene);
            switch (scene)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    if (_gameW != default)
                    {
                        _gameW.Destroy();
                    }

                    _menuW = new EcsWorld();
                    new Menu.FillEntitieSys(_menuW);
                    break;

                case SceneTypes.Game:
                    if (_menuW != default)
                    {
                        _menuW.Destroy();
                    }

                    _gameW = new EcsWorld();


                    var gameSysts = new EcsSystems(_gameW)
                        .Add(new SpawnEntities());

                    new Game.CreateDataS(gameSysts);
                    new DataMasSCreate(gameSysts);
                    new ViewDataSCreate(gameSysts);


                    var rpc_GO = new GameObject("RpcView");
                    var rpc = rpc_GO.AddComponent<RpcSys>();
                    //GenerZoneVC.Attach(rpc.transform);
                    new RpcVC(rpc_GO);

                    gameSysts.Add(rpc);

                    gameSysts.Init();

                    break;

                default: throw new Exception();
            }
        }
    }
}