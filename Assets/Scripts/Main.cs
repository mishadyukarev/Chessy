using Chessy.Common;
using Chessy.Game;
using Chessy.Menu;
using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Chessy
{
    public sealed class Main : MonoBehaviour
    {
        private SceneTypes _curScene;

        private EcsWorld _menuWorld;
        private EcsWorld _gameWorld;

        private void Start()
        {
            var comWorld = new EcsWorld();
            new Common.FillEntitiesSys(comWorld, ToggleScene, gameObject);

            _curScene = SceneTypes.Menu;
            ToggleScene(_curScene);

            //Application.runInBackground = false;
        }

        private void Update()
        {
            ComSysDataC.Invoke(ActionDataTypes.RunUpdate);

            switch (_curScene)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    MenuSysDataC.RunUpdate.Invoke();
                    break;

                case SceneTypes.Game:
                    DataC.RunUpdate.Invoke();
                    GameGenSysDataViewC.RunUpdate.Invoke();
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
                    if (_gameWorld != default)
                    {
                        _gameWorld.Destroy();
                        Game.ViewECreating.Dispose();
                    }

                    _menuWorld = new EcsWorld();
                    new Menu.FillEntitieSys(_menuWorld);
                    break;

                case SceneTypes.Game:
                    if (_menuWorld != default)
                    {
                        _menuWorld.Destroy();
                    }

                    _gameWorld = new EcsWorld();
                    var gameSysts = new EcsSystems(_gameWorld);

                    gameSysts
                        .Add(new ViewECreating())
                        .Add(new DataECreating())
                        .Add(new Spawn());



                    new DataSCreating(gameSysts);
                    new DataMasSCreating(gameSysts);
                    new ViewDataSCreating(gameSysts);

                    gameSysts.Init();

                    GameGenSysDataViewC.RotateAll.Invoke();

                    break;

                default: throw new Exception();
            }
        }
    }
}