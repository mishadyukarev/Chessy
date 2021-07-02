using Leopotam.Ecs;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    public sealed class ECSManager
    {
        private EcsWorld _commonWorld;
        private EcsWorld _menuWorld;
        private EcsWorld _gameWorld;

        private EntitiesCommonManager _entitiesCommonManager;


        private EntitiesGameGeneralManager _entitiesGameGeneralManager;
        private SystemsGameGeneralManager _systemsGameGeneralManager;

        private EntitiesGameMasterManager _entitiesGameMasterManager;
        private SystemsGameMasterManager _systemsGameMasterManager;

        private EntitiesGameOtherManager _entitiesGameOtherManager;
        private SystemsGameOtherManager _systemsGameOtherManager;



        public EntitiesCommonManager EntitiesCommonManager => _entitiesCommonManager;


        public EntitiesGameGeneralManager EntitiesGameGeneralManager => _entitiesGameGeneralManager;
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


            _entitiesGameGeneralManager = new EntitiesGameGeneralManager(_gameWorld, _entitiesCommonManager.ResourcesEnt_ResourcesCommonCom);
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
                case SceneTypes.Menu:
                    break;

                case SceneTypes.Game:
                    _systemsGameGeneralManager.Update();

                    if (Instance.IsMasterClient) _systemsGameMasterManager.Update();
                    else _systemsGameOtherManager.Update();
                    break;

                default:
                    break;
            }
        }

        public void ToggleScene(SceneTypes sceneType)
        {
            _entitiesCommonManager.ToggleScene(sceneType);

            switch (sceneType)
            {
                case SceneTypes.Menu:
                    _entitiesGameGeneralManager.Dispose();
                    break;

                case SceneTypes.Game:
                    _entitiesGameGeneralManager.FillEntities(_entitiesCommonManager.ResourcesEnt_ResourcesCommonCom);

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
                    break;
            }
        }
    }
}