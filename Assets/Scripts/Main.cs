using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Menu.Entities;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class Main : MonoBehaviour
    {
        #region Variables

        private bool _isOfflineMode = false;
        private SceneTypes _sceneType = SceneTypes.Menu;
        private static Main _instance;
        private PhotonMainManager _photonManager;
        private ECSManager _eCSmanager;

        //[NonSerialized]public GameModTypes GameModeType;

        #endregion


        #region Properties

        public static Main Instance => _instance;

        public bool IsOfflineMode => _isOfflineMode;
        public SceneTypes SceneType => _sceneType;

        public bool IsMasterClient => PhotonNetwork.IsMasterClient;
        public Player MasterClient => PhotonNetwork.MasterClient;
        public Player LocalPlayer => PhotonNetwork.LocalPlayer;

        public ECSManager ECSmanager => _eCSmanager;
        public PhotonMainManager PhotonManager => _photonManager;

        public ref CanvasCommComponent CanvasManager => ref _eCSmanager.EntitiesCommonManager.CanvasEnt_CanvasCommCom;
        public StartGameValuesConfig StartValuesGameConfig => _eCSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.StartValuesGameConfig;

        public EntitiesGameGeneralManager EntGGM => _eCSmanager.EntitiesGameGeneralManager;
        public EntitiesMenuManager EntMenuM => _eCSmanager.EntitiesMenuManager;

        #endregion


        private void Start()
        {
            _instance = this;

            _eCSmanager = new ECSManager();
            _photonManager = new PhotonMainManager(_eCSmanager);

            ToggleScene(_sceneType);
        }

        private void Update()
        {
            _eCSmanager.OwnUpdate(_sceneType);
            _photonManager.OwnUpdate(_sceneType);

            //Debug.Log(PhotonNetwork.CountOfPlayers);

            //Debug.Log(Instance.IsMasterClient);

            //Debug.Log(PhotonNetwork.CurrentRoom.Name);
        }

        private void OnApplicationQuit() { }

        public void ToggleScene(SceneTypes sceneType)
        {
            _sceneType = sceneType;

            _eCSmanager.ToggleScene(_sceneType);
            _photonManager.ToggleScene(_sceneType);
        }
    }
}