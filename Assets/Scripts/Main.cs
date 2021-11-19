using Game.Common;
using Game.Game;
using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Game
{
    public sealed class Main : MonoBehaviour
    {
        private SceneTypes _curScene;

        private EcsWorld _menuW;
        private EcsWorld _gameW;


        private void Start()
        {
            var comW = new EcsWorld();
            new Common.FillEntitiesSys(comW, ToggleScene, gameObject);

            _curScene = SceneTypes.Menu;
            ToggleScene(_curScene);

            //Application.runInBackground = false;
        }

        private void Update()
        {
            Common.DataSC.Invoke(ActionDataTypes.RunUpdate);

            switch (_curScene)
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
            _curScene = scene;
            switch (scene)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    if (_gameW != default)
                    {
                        _gameW.Destroy();
                        Game.ViewECreate.Dispose();
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
                    var gameSysts = new EcsSystems(_gameW);

                    gameSysts
                        .Add(new ViewECreate())
                        .Add(new ViewUIECreate())
                        .Add(new DataECreate())
                        .Add(new FillCells());



                    new DataSCreate(gameSysts);
                    new DataMasSCreate(gameSysts);
                    new ViewDataSCreate(gameSysts);

                    gameSysts.Init();

                    DataViewSC.RotateAll?.Invoke();

                    break;

                default: throw new Exception();
            }
        }
    }
}