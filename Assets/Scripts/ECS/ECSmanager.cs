using Assets.Scripts.Abstractions;
using Assets.Scripts.ECS.Entities.Game.General;
using Assets.Scripts.ECS.Manager.View.Common;
using Assets.Scripts.ECS.Manager.View.Menu;
using Assets.Scripts.ECS.Managers.Data.Common;
using Assets.Scripts.ECS.System.Common;
using Assets.Scripts.ECS.System.Data.Common;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.ECS.System.Data.Game.General.UI;
using Assets.Scripts.ECS.System.View.Common;
using Assets.Scripts.ECS.System.View.Game.General;
using Assets.Scripts.ECS.System.View.Game.General.Cell;
using Assets.Scripts.ECS.System.View.Game.General.UI;
using Assets.Scripts.ECS.System.View.Menu;
using Assets.Scripts.Workers.Common;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public sealed class ECSManager
    {
        internal EcsWorld CommonWorld { get; private set; }
        internal EcsWorld MenuWorld { get; private set; }
        internal EcsWorld GameWorld { get; private set; }


        #region Common

        public CommonElseViewSysManager CommElseViewSysManag { get; private set; }
        public CommonElseDataSysManager CommElseDataSysManag { get; private set; }

        #endregion


        #region Menu

        public MenuDataPhotonSceneSysManager PhotonSceneSysDataMenuManager { get; private set; }

        public UIMenuViewSysManager UIMenuViewSysMan { get; private set; }

        #endregion


        #region Game

        public ElseGameGeneralDataSysManager ElseGameGeneralDataSysManager { get; private set; }
        public SysViewGameGeneralElseManager SysViewGameGeneralElseManager { get; private set; }
        public SysDataGameGeneralCellManager SysDataGameGeneralCellManager { get; private set; }
        public SysViewGameGeneralCellManager SysViewGameGeneralCellManager { get; private set; }
        public SysDataGameGeneralUIManager SysDataGameGeneralUIManager { get; private set; }
        public SysViewGameGeneralUIManager SysViewGameGeneralUIManager { get; private set; }
        public SysDataGameMasterManager SysDataGameMasterManager { get; private set; }
        public SysDataGameOtherManager SysDataGameOtherManager { get; private set; }

        #endregion


        public ECSManager()
        {
            CommonWorld = new EcsWorld();

            CommElseDataSysManag = new CommonElseDataSysManager(CommonWorld);
            CommElseViewSysManag = new CommonElseViewSysManager(CommonWorld);

            CommElseDataSysManag.Init();
            CommElseViewSysManag.Init();
        }

        public void ToggleScene(SceneTypes sceneType)
        {
            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    if (GameWorld != default)
                    {
                        GameWorld.Destroy();

                        ElseGameGeneralDataSysManager = default;
                        SysDataGameMasterManager = default;
                        SysDataGameOtherManager = default;
                    }

                    MenuWorld = new EcsWorld();
                    UIMenuViewSysMan = new UIMenuViewSysManager(MenuWorld);
                    PhotonSceneSysDataMenuManager = new MenuDataPhotonSceneSysManager(MenuWorld);
                    UIMenuViewSysMan.Init();
                    PhotonSceneSysDataMenuManager.Init();



                    MainDataCommSys.ToggleZoneEnt_ParentCom.DestroyCurrentZone();
                    MainDataCommSys.ToggleZoneEnt_ParentCom.CreateZone(sceneType);

                    MainCommonViewSys.CanvasEnt_CanvasCom.DestroyZoneUI(SceneTypes.Game);

                    MainCommonViewSys.CanvasEnt_CanvasCom.SetZoneUI(SceneTypes.Menu, MainDataCommSys.ResourcesEnt_ResourcesCommonCom);

                    var slider = MainCommonViewSys.CanvasEnt_CanvasCom.FindUnderParent<Slider>(SceneTypes.Menu, "Slider");




                    break;

                case SceneTypes.Game:
                    MenuWorld.Destroy();

                    GameWorld = new EcsWorld();

                    if (PhotonNetwork.IsMasterClient)
                    {
                        if (MainDataCommSys.CommonZoneEnt_SaverCom.StepModeType == StepModeTypes.ByQueue)
                        {
                            DownDonerUIDataContainer.SetDoned(false, true);
                        }
                    }




                    ElseGameGeneralDataSysManager = new ElseGameGeneralDataSysManager(GameWorld);
                    SysViewGameGeneralElseManager = new SysViewGameGeneralElseManager(GameWorld);
                    SysDataGameGeneralCellManager = new SysDataGameGeneralCellManager(GameWorld);
                    SysViewGameGeneralCellManager = new SysViewGameGeneralCellManager(GameWorld);
                    SysDataGameGeneralUIManager = new SysDataGameGeneralUIManager(GameWorld);
                    SysViewGameGeneralUIManager = new SysViewGameGeneralUIManager(GameWorld);

                    SysDataGameMasterManager = new SysDataGameMasterManager(GameWorld);
                    SysDataGameOtherManager = new SysDataGameOtherManager(GameWorld);

                    ElseGameGeneralDataSysManager.Init();
                    SysViewGameGeneralElseManager.Init();
                    SysDataGameGeneralCellManager.Init();
                    SysViewGameGeneralCellManager.Init();

                    SysDataGameMasterManager.Init();
                    SysDataGameOtherManager.Init();



                    MainDataCommSys.ToggleZoneEnt_ParentCom.DestroyCurrentZone();
                    MainDataCommSys.ToggleZoneEnt_ParentCom.CreateZone(sceneType);

                    MainCommonViewSys.CanvasEnt_CanvasCom.DestroyZoneUI(SceneTypes.Menu);

                    MainCommonViewSys.CanvasEnt_CanvasCom.SetZoneUI(SceneTypes.Game, MainDataCommSys.ResourcesEnt_ResourcesCommonCom);

                    if (PhotonNetwork.IsMasterClient)
                    {
                        MainDataCommSys.CameraEnt_CameraCom.Camera.transform.rotation = new Quaternion(0, 0, 0, 0);
                        MainDataCommSys.CameraEnt_CameraCom.Camera.transform.position = Main.Instance.transform.position + MainDataCommSys.CameraEnt_CameraCom.PosForCamera;
                    }
                    else
                    {
                        MainDataCommSys.CameraEnt_CameraCom.Camera.transform.rotation = new Quaternion(0, 0, 180, 0);
                        MainDataCommSys.CameraEnt_CameraCom.Camera.transform.position = Main.Instance.transform.position + MainDataCommSys.CameraEnt_CameraCom.PosForCamera + new Vector3(0, 0.5f, 0);
                    }

                    break;

                default:
                    throw new Exception();
            }
        }


        public void OwnUpdate(SceneTypes sceneType)
        {
            CommElseDataSysManag.RunUpdate();
            CommElseViewSysManag.RunUpdate();

            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    PhotonSceneSysDataMenuManager.RunUpdate();

                    UIMenuViewSysMan.RunUpdate();
                    MainDataCommSys.CommonZoneEnt_SaverCom.SliderVolume = UIMenuMainViewSys.SoundEnt_SliderCom.Slider.value;
                    break;

                case SceneTypes.Game:
                    ElseGameGeneralDataSysManager.RunUpdate();

                    if (PhotonNetwork.IsMasterClient) SysDataGameMasterManager.RunUpdate();
                    else SysDataGameOtherManager.RunUpdate();
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}