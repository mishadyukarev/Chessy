using Assets.Scripts.ECS.Entities.Game.General;
using Assets.Scripts.ECS.Entities.Game.General.Else.Vis;
using Assets.Scripts.ECS.Entity.Data.Common.UI;
using Assets.Scripts.ECS.Entity.View.Common;
using Assets.Scripts.ECS.Entity.View.Common.UI;
using Assets.Scripts.ECS.Menu.Entities;
using Assets.Scripts.ECS.System.Common;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.ECS.System.Data.Game.General.UI;
using Assets.Scripts.ECS.System.View.Game.General;
using Assets.Scripts.ECS.System.View.Game.General.Cell;
using Assets.Scripts.ECS.System.View.Game.General.UI;
using Assets.Scripts.Workers.Common;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using Photon.Pun;
using System;

namespace Assets.Scripts
{
    public sealed class ECSManager
    {
        #region Worlds

        private EcsWorld _commonWorld;
        private EcsWorld _menuWorld;
        private EcsWorld _gameWorld;

        #endregion


        #region Entities

        #region Common

        public EntDataCommonElseManager EntDataCommElseManager { get; private set; }
        public EntViewCommonElseManager EntViewCommElseManager { get; private set; }
        public EntDataCommonUIManager EntDataCommUIManager { get; private set; }
        public EntViewCommonUIManager EntViewCommUIManager { get; private set; }

        #endregion


        #region Menu

        public EntViewMenuElseManager EntViewMenuElseManager { get; private set; }

        #endregion


        #region Game

        public EntDataGameGeneralElseManager EntDataGameGeneralElseManager { get; private set; }
        public EntViewGameGeneralElseManager EntViewGameGeneralElseManager { get; private set; }
        public EntDataGameGeneralUIManager EntDataGameGeneralUIManager { get; private set; }
        public EntViewGameGeneralUIManager EntViewGameGeneralUIManager { get; private set; }
        public EntDataGameMasterElseManager EntDataGameMasterElseManager { get; private set; }
        public EntDataGameOtherElseManager EntDataGameOtherElseManager { get; private set; }

        #endregion

        #endregion


        #region Systems

        #region Common

        public SysDataCommonManager SysCommManager { get; private set; }

        #endregion


        #region Game
        public SysDataGameGeneralElseManager SysDataGameGeneralElseManager { get; private set; }
        public SysViewGameGeneralElseManager SysViewGameGeneralElseManager { get; private set; }
        public SysDataGameGeneralCellManager SysDataGameGeneralCellManager { get; private set; }
        public SysViewGameGeneralCellManager SysViewGameGeneralCellManager { get; private set; }
        public SysDataGameGeneralUIManager SysDataGameGeneralUIManager { get; private set; }
        public SysViewGameGeneralUIManager SysViewGameGeneralUIManager { get; private set; }
        public SysGameMasterManager SysGameMasterManager { get; private set; }
        public SysGameOtherManager SysGameOtherManager { get; private set; }

        #endregion

        #endregion


        public ECSManager()
        {
            _commonWorld = new EcsWorld();
            EntDataCommElseManager = new EntDataCommonElseManager(_commonWorld);
            EntViewCommElseManager = new EntViewCommonElseManager(_commonWorld, EntDataCommElseManager);
            EntDataCommUIManager = new EntDataCommonUIManager(_commonWorld);
            EntViewCommUIManager = new EntViewCommonUIManager(_commonWorld, EntDataCommElseManager);

            SysCommManager = new SysDataCommonManager(_commonWorld);
            SysCommManager.ProcessInjects();
            SysCommManager.Init();
        }

        public void OwnUpdate(SceneTypes sceneType)
        {
            EntDataCommElseManager.OwnUpdate(sceneType, EntViewMenuElseManager);
            EntViewCommElseManager.OwnUpdate(sceneType);

            SysCommManager.RunUpdate();

            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    DataCommContainerElseSaver.SliderVolume = EntViewMenuElseManager.SoundEnt_SliderCom.Slider.value;
                    break;

                case SceneTypes.Game:
                    SysDataGameGeneralElseManager.RunUpdate();

                    if (PhotonNetwork.IsMasterClient) SysGameMasterManager.RunUpdate();
                    else SysGameOtherManager.RunUpdate();
                    break;

                default:
                    throw new Exception();
            }
        }

        public void ToggleScene(SceneTypes sceneType)
        {
            EntDataCommElseManager.ToggleScene(sceneType);

            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    if (_gameWorld != default)
                    {
                        _gameWorld.Destroy();

                        EntDataGameGeneralElseManager = default;
                        EntViewGameGeneralElseManager = default;
                        EntViewGameGeneralUIManager = default;
                        EntDataGameMasterElseManager = default;
                        EntDataGameOtherElseManager = default;

                        SysDataGameGeneralElseManager = default;
                        SysGameMasterManager = default;
                        SysGameOtherManager = default;
                    }

                    _menuWorld = new EcsWorld();
                    EntViewMenuElseManager = new EntViewMenuElseManager(_menuWorld);
                    break;

                case SceneTypes.Game:
                    _menuWorld.Destroy();
                    EntViewMenuElseManager = default;


                    _gameWorld = new EcsWorld();

                    EntDataGameGeneralElseManager = new EntDataGameGeneralElseManager(_gameWorld, EntDataCommElseManager);
                    EntViewGameGeneralElseManager = new EntViewGameGeneralElseManager(_gameWorld, EntDataCommElseManager);
                    EntDataGameGeneralUIManager = new EntDataGameGeneralUIManager(_gameWorld);
                    EntViewGameGeneralUIManager = new EntViewGameGeneralUIManager(_gameWorld);
                    EntDataGameMasterElseManager = new EntDataGameMasterElseManager(_gameWorld);
                    EntDataGameOtherElseManager = new EntDataGameOtherElseManager(_gameWorld);

                    if (PhotonNetwork.IsMasterClient)
                    {
                        if (DataCommContainerElseSaver.StepModeType == StepModeTypes.ByQueue)
                        {
                            DownDonerUIDataContainer.SetDoned(false, true);
                        }
                    }




                    SysDataGameGeneralElseManager = new SysDataGameGeneralElseManager(_gameWorld);
                    SysViewGameGeneralElseManager = new SysViewGameGeneralElseManager(_gameWorld);
                    SysDataGameGeneralCellManager = new SysDataGameGeneralCellManager(_gameWorld);
                    SysViewGameGeneralCellManager = new SysViewGameGeneralCellManager(_gameWorld);

                    SysGameMasterManager = new SysGameMasterManager(_gameWorld);
                    SysGameOtherManager = new SysGameOtherManager(_gameWorld);

                    SysDataGameGeneralElseManager.ProcessInjects();
                    SysViewGameGeneralElseManager.ProcessInjects();
                    SysDataGameGeneralCellManager.ProcessInjects();
                    SysViewGameGeneralCellManager.ProcessInjects();
                    SysGameMasterManager.ProcessInjects();
                    SysGameOtherManager.ProcessInjects();

                    SysDataGameGeneralElseManager.Init();
                    SysViewGameGeneralElseManager.Init();
                    SysDataGameGeneralCellManager.Init();
                    SysViewGameGeneralCellManager.Init();
                    SysGameMasterManager.Init();
                    SysGameOtherManager.Init();

                    break;

                default:
                    throw new Exception();
            }
        }
    }
}