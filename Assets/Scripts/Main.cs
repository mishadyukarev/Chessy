using Game.Common;
using Game.Game;
using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Game
{
    public sealed class Main : MonoBehaviour
    {
        private EcsWorld _menuW;
        private EcsWorld _gameW;


        private void Start()
        {
            new Common.CreateVCs(transform);

            var comSysts = new EcsSystems(new EcsWorld());
            new Common.CreateVSs(comSysts, gameObject);
            new Common.CreateSysts(comSysts, ToggleScene);

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



                    new Game.CreateVCs(transform);
                    new CreateCs();

                    var gameSysts = new EcsSystems(_gameW)
                        .Add(new CreateVEnts())
                        .Add(new CreateEnts())
                        .Add(new FillCells());

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