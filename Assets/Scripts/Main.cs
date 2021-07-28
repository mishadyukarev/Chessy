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
        #region Variables

        private SceneTypes _currentSceneType = SceneTypes.Menu;
        private static Main _instance;
        private PhotonMainManager _photonManager;
        private ECSManager _eCSmanager;

        public const string VERSION_PHOTON_GAME = "0.1i";

        #endregion


        #region Properties

        public static Main Instance => _instance;

        public SceneTypes CurrentSceneType => _currentSceneType;

        internal bool IsStarted { get; set; } = true;

        public bool IsMasterClient => PhotonNetwork.IsMasterClient;
        public Player MasterClient => PhotonNetwork.MasterClient;
        public Player LocalPlayer => PhotonNetwork.LocalPlayer;

        public ECSManager ECSmanager => _eCSmanager;
        public PhotonMainManager PhotonManager => _photonManager;

        public ref CanvasComponent CanvasManager => ref _eCSmanager.EntitiesCommonManager.CanvasEnt_CanvasCommCom;
        public StartGameValuesConfig StartValuesGameConfig => _eCSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.StartValuesGameConfig;

        public EntitiesGameGeneralManager EntGGM => _eCSmanager.EntitiesGameGeneralManager;
        public EntitiesGameGeneralUIManager EntGGUIM => _eCSmanager.EntitiesGameGeneralUIManager;
        public EntitiesMenuManager EntMenuM => _eCSmanager.EntitiesMenuManager;
        public EntitiesCommonManager EntComM => _eCSmanager.EntitiesCommonManager;

        #endregion


        private void Start()
        {
            _instance = this;

            _eCSmanager = new ECSManager();
            _photonManager = new PhotonMainManager();

            ToggleScene(_currentSceneType);
        }

        private void Update()
        {
            _eCSmanager.OwnUpdate(_currentSceneType);
            _photonManager.OwnUpdate(_currentSceneType);

            switch (_currentSceneType)
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
            _currentSceneType = sceneType;

            _eCSmanager.ToggleScene(sceneType);
            _photonManager.ToggleScene(sceneType);

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