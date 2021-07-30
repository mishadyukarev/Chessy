using Assets.Scripts.ECS.Entities.Game.General;
using Assets.Scripts.ECS.Entities.Game.General.Cell;
using Assets.Scripts.ECS.Entities.Game.General.Cells.View;
using Assets.Scripts.ECS.Entities.Game.General.Else.Vis;
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


        #region Entities

        public EntCommonManager EntCommonManager { get; private set; }

        public EntMenuManager EntMenuManager { get; private set; }

        public EntGameGeneralElseDataManager EntGameGeneralElseDataManager { get; private set; }
        public EntGameGeneralElseViewManager EntGameGeneralElseViewManager { get; private set; }
        public EntGameGeneralCellDataManager EntGameGeneralCellDataManager { get; private set; }
        public EntGameGeneralCellViewManager EntGameGeneralCellViewManager { get; private set; }
        public EntGameGeneralUIDataManager EntGameGeneralUIDataManager { get; private set; }
        public EntitiesGameGeneralUIViewManager EntGameGeneralUIViewManager { get; private set; }
        public EntitiesGameMasterManager EntGameMasterManager { get; private set; }
        public EntitiesGameOtherManager EntGameOtherManager { get; private set; }

        #endregion



        public SystemsGameMasterManager SysGameMasterManager { get; private set; }
        public SystemsGameGeneralManager SysGameGeneralManager { get; private set; }
        public SystemsGameOtherManager SysGameOtherManager { get; private set; }

        public ECSManager()
        {
            _commonWorld = new EcsWorld();
            _menuWorld = new EcsWorld();
            _gameWorld = new EcsWorld();


            EntCommonManager = new EntCommonManager(_commonWorld);
        }

        public void OwnUpdate(SceneTypes sceneType)
        {
            EntCommonManager.OwnUpdate(sceneType);

            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    EntCommonManager.SaverEnt_SaverCommCom.SliderVolume = EntMenuManager.SoundEnt_SliderCom.Slider.value;
                    break;

                case SceneTypes.Game:
                    SysGameGeneralManager.Update();

                    if (Instance.IsMasterClient) SysGameMasterManager.Update();
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
                    EntMenuManager = new EntMenuManager(_menuWorld);
                    break;

                case SceneTypes.Game:
                    _menuWorld.Destroy();
                    EntMenuManager = default;


                    _gameWorld = new EcsWorld();

                    EntGameGeneralElseDataManager = new EntGameGeneralElseDataManager(_gameWorld);
                    EntGameGeneralElseViewManager = new EntGameGeneralElseViewManager(_gameWorld);
                    EntGameGeneralCellDataManager = new EntGameGeneralCellDataManager(_gameWorld);
                    EntGameGeneralCellViewManager = new EntGameGeneralCellViewManager(_gameWorld);
                    EntGameGeneralUIViewManager = new EntitiesGameGeneralUIViewManager(_gameWorld);
                    EntGameGeneralUIDataManager = new EntGameGeneralUIDataManager(_gameWorld);
                    EntGameMasterManager = new EntitiesGameMasterManager(_gameWorld);
                    EntGameOtherManager = new EntitiesGameOtherManager(_gameWorld);

                    if (PhotonNetwork.IsMasterClient)
                    {
                        if (Instance.EntComM.SaverEnt_StepModeTypeCom.StepModeType == StepModeTypes.ByQueue)
                        {
                            DownDonerUIWorker.SetDoned(false, true);
                        }
                    }




                    SysGameGeneralManager = new SystemsGameGeneralManager(_gameWorld);
                    SysGameMasterManager = new SystemsGameMasterManager(_gameWorld);
                    SysGameOtherManager = new SystemsGameOtherManager(_gameWorld);

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