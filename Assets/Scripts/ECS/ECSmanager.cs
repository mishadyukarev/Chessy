using Assets.Scripts.ECS.Entities.Game.General;
using Assets.Scripts.ECS.Entities.Game.General.Cell;
using Assets.Scripts.ECS.Entities.Game.General.Cells.View;
using Assets.Scripts.ECS.Entities.Game.General.Else.Vis;
using Assets.Scripts.ECS.Menu.Entities;
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

        public EntCommonManager EntCommonManager { get; private set; }
        public EntMenuManager EntMenuManager { get; private set; }


        #region Game

        public EntGameGeneralElseDataManager EntGameGeneralElseDataManager { get; private set; }
        public EntGameGeneralElseViewManager EntGameGeneralElseViewManager { get; private set; }
        public EntGameGeneralCellDataManager EntGameGeneralCellDataManager { get; private set; }
        public EntGameGeneralCellViewManager EntGameGeneralCellViewManager { get; private set; }
        public EntGameGeneralUIDataManager EntGameGeneralUIDataManager { get; private set; }
        public EntitiesGameGeneralUIViewManager EntGameGeneralUIViewManager { get; private set; }
        public EntitiesGameMasterManager EntGameMasterManager { get; private set; }
        public EntitiesGameOtherManager EntGameOtherManager { get; private set; }

        #endregion

        #endregion


        #region Systems

        public SystemsGameMasterManager SysGameMasterManager { get; private set; }
        public SystemsGameGeneralManager SysGameGeneralManager { get; private set; }
        public SystemGameOtherManager SysGameOtherManager { get; private set; }

        #endregion


        public ECSManager()
        {
            _commonWorld = new EcsWorld();
            EntCommonManager = new EntCommonManager(_commonWorld);
        }

        public void OwnUpdate(SceneTypes sceneType)
        {
            EntCommonManager.OwnUpdate(sceneType, EntMenuManager);

            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    EntCommonManager.SaverEnt_SaverCommCom.SliderVolume = EntMenuManager.SoundEnt_SliderCom.Slider.value;
                    break;

                case SceneTypes.Game:
                    SysGameGeneralManager.Update();

                    if (PhotonNetwork.IsMasterClient) SysGameMasterManager.Update();
                    else SysGameOtherManager.Update();
                    break;

                default:
                    throw new Exception();
            }
        }

        public void ToggleScene(SceneTypes sceneType)
        {
            EntCommonManager.ToggleScene(sceneType);

            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    if (_gameWorld != default)
                    {
                        _gameWorld.Destroy();

                        EntGameGeneralElseDataManager = default;
                        EntGameGeneralElseViewManager = default;
                        EntGameGeneralCellDataManager = default;
                        EntGameGeneralCellViewManager = default;
                        EntGameGeneralUIViewManager = default;
                        EntGameMasterManager = default;
                        EntGameOtherManager = default;

                        SysGameGeneralManager = default;
                        SysGameMasterManager = default;
                        SysGameOtherManager = default;
                    }

                    _menuWorld = new EcsWorld();
                    EntMenuManager = new EntMenuManager(_menuWorld, EntCommonManager);
                    break;

                case SceneTypes.Game:
                    _menuWorld.Destroy();
                    EntMenuManager = default;


                    _gameWorld = new EcsWorld();

                    EntGameGeneralElseDataManager = new EntGameGeneralElseDataManager(_gameWorld, EntCommonManager);
                    EntGameGeneralElseViewManager = new EntGameGeneralElseViewManager(_gameWorld, EntCommonManager);
                    EntGameGeneralCellDataManager = new EntGameGeneralCellDataManager(_gameWorld);
                    EntGameGeneralCellViewManager = new EntGameGeneralCellViewManager(_gameWorld, EntCommonManager);
                    EntGameGeneralUIViewManager = new EntitiesGameGeneralUIViewManager(_gameWorld);
                    EntGameGeneralUIDataManager = new EntGameGeneralUIDataManager(_gameWorld);
                    EntGameMasterManager = new EntitiesGameMasterManager(_gameWorld);
                    EntGameOtherManager = new EntitiesGameOtherManager(_gameWorld);

                    if (PhotonNetwork.IsMasterClient)
                    {
                        if (SaverComWorker.StepModeType == StepModeTypes.ByQueue)
                        {
                            DownDonerUIWorker.SetDoned(false, true);
                        }
                    }




                    SysGameGeneralManager = new SystemsGameGeneralManager(_gameWorld);
                    SysGameMasterManager = new SystemsGameMasterManager(_gameWorld);
                    SysGameOtherManager = new SystemGameOtherManager(_gameWorld);

                    SysGameGeneralManager.ProcessInjects();
                    SysGameMasterManager.ProcessInjects();
                    SysGameOtherManager.ProcessInjects();

                    SysGameGeneralManager.Init();
                    SysGameMasterManager.Init();
                    SysGameOtherManager.Init();

                    break;

                default:
                    throw new Exception();
            }
        }
    }
}