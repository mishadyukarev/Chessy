using Assets.Scripts.ECS.Menu.Entities;
using Photon.Pun;
using Photon.Realtime;
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

        public bool IsMasterClient => PhotonNetwork.IsMasterClient;
        public Player MasterClient => PhotonNetwork.MasterClient;
        public Player LocalPlayer => PhotonNetwork.LocalPlayer;

        public ECSManager ECSmanager => _eCSmanager;
        public PhotonMainManager PhotonManager => _photonManager;

        public ref CanvasCommComponent CanvasManager => ref _eCSmanager.EntitiesCommonManager.CanvasEnt_CanvasCommCom;
        public StartGameValuesConfig StartValuesGameConfig => _eCSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.StartValuesGameConfig;

        public EntitiesGameGeneralManager EntGGM => _eCSmanager.EntitiesGameGeneralManager;
        public EntitiesMenuManager EntMenuM => _eCSmanager.EntitiesMenuManager;
        public EntitiesCommonManager EntComM => _eCSmanager.EntitiesCommonManager;

        #endregion


        private void Start()
        {
            _instance = this;

            _eCSmanager = new ECSManager();
            _photonManager = new PhotonMainManager(_eCSmanager);

            ToggleScene(_currentSceneType);
        }

        private void Update()
        {
            _eCSmanager.OwnUpdate(_currentSceneType);
            _photonManager.OwnUpdate(_currentSceneType);
        }

        private void OnApplicationQuit() { }

        public void ToggleScene(SceneTypes sceneType)
        {
            _currentSceneType = sceneType;

            _eCSmanager.ToggleScene(_currentSceneType);
            _photonManager.ToggleScene(_currentSceneType);
        }
    }
}