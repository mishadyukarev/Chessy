using Leopotam.Ecs;
using Chessy.Common;
using Chessy.Game;
using Chessy.Menu;
using System;
using UnityEngine;

namespace Chessy
{
    public sealed class ECSManager
    {
        #region 

        private EcsWorld _comWorld;
        private EcsWorld _menuWorld;
        private EcsWorld _gameWorld;

        #endregion


        public ECSManager(Action<SceneTypes> toggleScene, GameObject main_GO)
        {
            _comWorld = new EcsWorld();
            new Common.FillEntitiesSys(_comWorld, toggleScene, main_GO);
        }

        public void ToggleScene(SceneTypes sceneType)
        {
            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    if (_gameWorld != default)
                    {
                        _gameWorld.Destroy();
                        Game.Ents.Dispose();
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

                    gameSysts.Add(new Ents());
                    new DataS(gameSysts);
                    new DataMasS(gameSysts);
                    new ViewDataS(gameSysts);

                    gameSysts.Init();

                    GameGenSysDataViewC.RotateAll.Invoke();

                    break;

                default: throw new Exception();
            }
        }


        public void OwnUpdate(SceneTypes sceneType)
        {
            ComSysDataC.Invoke(ActionDataTypes.RunUpdate);

            switch (sceneType)
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
    }
}