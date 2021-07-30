using Assets.Scripts.ECS.Menu.Entities;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class Main : MonoBehaviour
    {
        public const string VERSION_PHOTON_GAME = "0.1i";


        #region Properties

        public static Main Instance { get; private set; }

        public SceneTypes CurrentSceneType { get; private set; } = SceneTypes.Menu;

        public bool IsMasterClient => PhotonNetwork.IsMasterClient;
        public Player MasterClient => PhotonNetwork.MasterClient;
        public Player LocalPlayer => PhotonNetwork.LocalPlayer;

        public ECSManager ECSmanager { get; private set; }
        public PhotonMainManager PhotonManager { get; private set; }

        public ref CanvasComponent CanvasManager => ref ECSmanager.EntCommonManager.CanvasEnt_CanvasCommCom;
        public StartGameValuesConfig StartValuesGameConfig => ECSmanager.EntCommonManager.ResourcesEnt_ResourcesCommonCom.StartValuesGameConfig;

        public EntGameGeneralElseDataManager EntGGM => ECSmanager.EntGameGeneralElseDataManager;
        public EntitiesGameGeneralUIViewManager EntGGUIM => ECSmanager.EntGameGeneralUIViewManager;
        public EntMenuManager EntMenuM => ECSmanager.EntMenuManager;
        public EntCommonManager EntComM => ECSmanager.EntCommonManager;

        #endregion


        private void Start()
        {
            Instance = this;

            ECSmanager = new ECSManager();
            PhotonManager = new PhotonMainManager();

            ToggleScene(CurrentSceneType);
        }

        private void Update()
        {
            ECSmanager.OwnUpdate(CurrentSceneType);
            PhotonManager.OwnUpdate(CurrentSceneType);

            switch (CurrentSceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    break;

                case SceneTypes.Game:
                    Debug.Log(InfoAmountUnitsWorker.GetAmountUnitsInGame(UnitTypes.Pawn, PhotonNetwork.IsMasterClient));
                    break;

                default:
                    throw new Exception();
            }
        }

        private void OnApplicationQuit() { }

        public void ToggleScene(SceneTypes sceneType)
        {
            CurrentSceneType = sceneType;

            ECSmanager.ToggleScene(sceneType);
            PhotonManager.ToggleScene(sceneType);

            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    break;

                case SceneTypes.Game:
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}