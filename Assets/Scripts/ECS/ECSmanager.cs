using Assets.Scripts.ECS.Menu.Entities;
using Leopotam.Ecs;
using System;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    public sealed class ECSManager
    {
        private EcsWorld _commonWorld;
        private EcsWorld _menuWorld;
        private EcsWorld _gameWorld;

        private EntitiesCommonManager _entitiesCommonManager;


        private EntitiesMenuManager _entitiesMenuManager;


        private EntitiesGameGeneralManager _entitiesGameGeneralManager;
        private EntitiesGameGeneralUIManager _entitiesGameGeneralUIManager;
        private SystemsGameGeneralManager _systemsGameGeneralManager;

        private EntitiesGameMasterManager _entitiesGameMasterManager;
        private SystemsGameMasterManager _systemsGameMasterManager;

        private EntitiesGameOtherManager _entitiesGameOtherManager;
        private SystemsGameOtherManager _systemsGameOtherManager;



        public EntitiesCommonManager EntitiesCommonManager => _entitiesCommonManager;


        public EntitiesMenuManager EntitiesMenuManager => _entitiesMenuManager;


        public EntitiesGameGeneralManager EntitiesGameGeneralManager => _entitiesGameGeneralManager;
        public EntitiesGameGeneralUIManager EntitiesGameGeneralUIManager => _entitiesGameGeneralUIManager;
        public SystemsGameGeneralManager SystemsGameGeneralManager => _systemsGameGeneralManager;

        public EntitiesGameMasterManager EntitiesGameMasterManager => _entitiesGameMasterManager;
        public SystemsGameMasterManager SystemsGameMasterManager => _systemsGameMasterManager;

        public EntitiesGameOtherManager EntitiesGameOtherManager => _entitiesGameOtherManager;
        public SystemsGameOtherManager SystemsGameOtherManager => _systemsGameOtherManager;

        public ECSManager()
        {
            _commonWorld = new EcsWorld();
            _menuWorld = new EcsWorld();
            _gameWorld = new EcsWorld();


            _entitiesCommonManager = new EntitiesCommonManager(_commonWorld);


            _entitiesMenuManager = new EntitiesMenuManager(_menuWorld);


            _entitiesGameGeneralManager = new EntitiesGameGeneralManager();
            _entitiesGameGeneralUIManager = new EntitiesGameGeneralUIManager(_gameWorld);
            _systemsGameGeneralManager = new SystemsGameGeneralManager();

            _entitiesGameMasterManager = new EntitiesGameMasterManager(_gameWorld);
            _systemsGameMasterManager = new SystemsGameMasterManager();

            _entitiesGameOtherManager = new EntitiesGameOtherManager(_gameWorld);
            _systemsGameOtherManager = new SystemsGameOtherManager();
        }

        public void OwnUpdate(SceneTypes sceneType)
        {
            _entitiesCommonManager.OwnUpdate(sceneType);

            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    break;

                case SceneTypes.Game:
                    _systemsGameGeneralManager.Update();

                    if (Instance.IsMasterClient) _systemsGameMasterManager.Update();
                    else _systemsGameOtherManager.Update();
                    break;

                default:
                    throw new Exception();
            }
        }

        public void ToggleScene(SceneTypes sceneType)
        {
            _entitiesCommonManager.ToggleScene(sceneType);

            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    if (!Instance.IsStarted) _gameWorld.Destroy();

                    _entitiesMenuManager.FillEntities();

                    Instance.IsStarted = false;
                    break;

                case SceneTypes.Game:
                    _gameWorld = new EcsWorld();
                    _entitiesGameGeneralManager.FillEntities(_gameWorld);
                    _entitiesGameGeneralUIManager.FillEntities(_gameWorld);
                    _entitiesGameMasterManager.FillEntities(_gameWorld);
                    _entitiesGameOtherManager.FillEntities(_gameWorld);


                    _systemsGameGeneralManager.DestroySystems();
                    _systemsGameMasterManager.DestroySystems();
                    _systemsGameOtherManager.DestroySystems();

                    _systemsGameGeneralManager.CreateSystems(_gameWorld);
                    _systemsGameMasterManager.CreateSystems(_gameWorld);
                    _systemsGameOtherManager.CreateSystems(_gameWorld);

                    _systemsGameGeneralManager.ProcessInjects();
                    _systemsGameMasterManager.ProcessInjects();
                    _systemsGameOtherManager.ProcessInjects();

                    _systemsGameGeneralManager.Init();
                    _systemsGameMasterManager.Init();
                    _systemsGameOtherManager.Init();
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}