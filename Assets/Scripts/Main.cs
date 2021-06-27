using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class Main : MonoBehaviour
    {
        #region Variables

        [SerializeField] private bool _isOfflineMode;
        [SerializeField] private TestTypes _testType;
        [SerializeField] private BotTypes _botType;
        private SceneTypes _sceneType = SceneTypes.Menu;

        private static Main _instance;
        private PhotonManager _photonManager;
        private ECSManager _eCSmanager;

        #endregion


        #region Properties

        public static Main Instance => _instance;

        public bool IsOfflineMode => _isOfflineMode;
        public TestTypes TestType => _testType;
        public BotTypes BotType => _botType;
        public SceneTypes SceneType => _sceneType;

        public bool IsMasterClient => PhotonNetwork.IsMasterClient;
        public Player MasterClient => PhotonNetwork.MasterClient;
        public Player LocalPlayer => PhotonNetwork.LocalPlayer;

        public ECSManager ECSmanager => _eCSmanager;
        public PhotonManager PhotonManager => _photonManager;

        public ref CanvasCommComponent CanvasManager => ref _eCSmanager.EntitiesCommonManager.CanvasEnt_CanvasCommCom;
        public StartGameValuesConfig StartValuesGameConfig => _eCSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.StartValuesGameConfig;

        #endregion


        private void Start()
        {
            _instance = this;

            _eCSmanager = new ECSManager(); 
            _photonManager = new PhotonManager(_eCSmanager);

            ToggleScene(_sceneType);
        }

        private void Update()
        {
            Debug.Log(PhotonNetwork.CountOfRooms);

            _eCSmanager.OwnUpdate(_sceneType);
            _photonManager.OwnUpdate(_sceneType);
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