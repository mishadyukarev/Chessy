using Game.Common;
using Game.Game;
using Leopotam.Ecs;
using Photon.Pun;
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
            var comW = new EcsWorld();
            var comSysts = new EcsSystems(comW);
            new Common.FillEntitiesSys(comSysts, ToggleScene, gameObject);

            ToggleScene(SceneTypes.Menu);
        }

        private void Update()
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
                    Game.DataSC.RunUpdate();
                    DataMastSC.RunUpdate();
                    DataViewSC.RunUpdate();
                    break;

                default:
                    throw new Exception();
            }
        }

        private void ToggleScene(SceneTypes scene)
        {
            CurSceneC.Scene = scene;
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



                    new CreateVCs();
                    new CreateCs();

                    var gameSysts = new EcsSystems(_gameW)
                        .Add(new CreateVEnts())
                        .Add(new CreateEnts())
                        .Add(new FillCells());

                    new DataSCreate(gameSysts);
                    new DataMasSCreate(gameSysts);
                    new ViewDataSCreate(gameSysts);


                    gameSysts.Add(RpcVC.RpcView_GO.GetComponent<RpcSys>());

                    gameSysts.Init();

                    DataViewSC.RotateAll?.Invoke();

                    

                    break;

                default: throw new Exception();
            }
        }
    }
}