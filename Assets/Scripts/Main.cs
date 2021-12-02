using Game.Common;
using Game.Game;
using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Game
{
    public sealed class Main : MonoBehaviour
    {
        [SerializeField] TestModes _testMode;

        EcsWorld _menuW;
        EcsWorld _gameW;


        private void Start()
        {
            new Common.CreateCs(transform, _testMode);

            var comSysts = new EcsSystems(new EcsWorld());
            new Common.CreateVSs(comSysts, gameObject);
            new Common.CreateSs(comSysts, ToggleScene);

            comSysts.Init();

            ToggleScene(SceneTypes.Menu);
        }

        private void Update()
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


                    new Game.CreateVCs(MainGoVC.Rot);
                    new Game.CreateCs();

                    var gameSysts = new EcsSystems(_gameW)
                        .Add(new SpawnEntities());

                    new Game.CreateDataS(gameSysts);
                    new DataMasSCreate(gameSysts);
                    new ViewDataSCreate(gameSysts);


                    gameSysts.Add(RpcVC.RpcView_GO.GetComponent<RpcSys>());

                    gameSysts.Init();

                    break;

                default: throw new Exception();
            }
        }
    }
}