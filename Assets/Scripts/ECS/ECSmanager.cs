using Assets.Scripts.ECS.Menu.Entities;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using Photon.Pun;
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
        }

        public void OwnUpdate(SceneTypes sceneType)
        {
            _entitiesCommonManager.OwnUpdate(sceneType);

            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    _entitiesCommonManager.SaverEnt_SaverCommCom.SliderVolume = _entitiesMenuManager.SoundEnt_SliderCom.Slider.value;
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
                    if (_gameWorld != default)
                    {
                        _gameWorld.Destroy();

                        _entitiesGameGeneralManager = default;
                        _entitiesGameGeneralUIManager = default;
                        _entitiesGameMasterManager = default;
                        _entitiesGameOtherManager = default;

                        _systemsGameGeneralManager = default;
                        _systemsGameMasterManager = default;
                        _systemsGameOtherManager = default;
                    }

                    _menuWorld = new EcsWorld();

                    _entitiesMenuManager = new EntitiesMenuManager(_menuWorld);

                    Instance.IsStarted = false;
                    break;

                case SceneTypes.Game:
                    _menuWorld.Destroy();
                    _entitiesMenuManager = default;


                    _gameWorld = new EcsWorld();

                    _entitiesGameGeneralManager = new EntitiesGameGeneralManager(_gameWorld);
                    _entitiesGameGeneralUIManager = new EntitiesGameGeneralUIManager(_gameWorld);

                    if (PhotonNetwork.IsMasterClient)
                    {
                        if (Instance.EntComM.SaverEnt_StepModeTypeCom.StepModeType == StepModeTypes.ByQueue)
                        {
                            DownDonerUIWorker.SetDoned(false, true);
                        }
                    }


                    _entitiesGameMasterManager = new EntitiesGameMasterManager(_gameWorld);

                    _entitiesGameOtherManager = new EntitiesGameOtherManager(_gameWorld);


                    _systemsGameGeneralManager = new SystemsGameGeneralManager(_gameWorld);
                    _systemsGameMasterManager = new SystemsGameMasterManager(_gameWorld);
                    _systemsGameOtherManager = new SystemsGameOtherManager(_gameWorld);

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