using Leopotam.Ecs;
using Scripts.Common;
using Scripts.Game;
using Scripts.Menu;
using System;
using UnityEngine;

namespace Scripts
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
                        Game.FillEntitiesSys.Dispose();
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
                    new Game.FillEntitiesSys(new EcsSystems(_gameWorld));
                    break;

                default: throw new Exception();
            }
        }


        public void OwnUpdate(SceneTypes sceneType)
        {
            ComSysDataC.Invoke(EventDataTypes.RunUpdate);

            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    MenuSysDataC.RunUpdate.Invoke();
                    break;

                case SceneTypes.Game:
                    GameGenSysDataC.RunUpdate.Invoke();
                    GameGenSysDataViewC.RunUpdate.Invoke();
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}