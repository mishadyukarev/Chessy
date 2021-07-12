using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    public sealed class PhotonSceneManager : MonoBehaviourPunCallbacks
    {

        #region Menu

        private TextMeshProUGUI _logTex;
        private const byte MAX_PLAYERS = 2;
        //private LoadBalancingClient _loadBalancingClient = new LoadBalancingClient();
        private bool _inCreatingRoom;

        internal void Constructor()
        {

        }

        internal void OwnUpdate()
        {

        }


        internal void ToggleScene(SceneTypes sceneType)
        {
            switch (sceneType)
            {
                case SceneTypes.Menu:
                    _logTex = Instance.CanvasManager.FindUnderParent<TextMeshProUGUI>(SceneTypes.Menu, "LogText");

                    Instance.CanvasManager.FindUnderParent<Button>(sceneType, "CreateRoomButton").onClick.AddListener(CreateRoom);
                    Instance.CanvasManager.FindUnderParent<Button>(sceneType, "JoinRandomButton").onClick.AddListener(JoinRandomRoom);
                    Instance.CanvasManager.FindUnderParent<Button>(sceneType, "QuitButton").onClick.AddListener(delegate { Application.Quit(); });
                    Instance.CanvasManager.FindUnderParent<Button>(sceneType, "CreateTestGameButton").onClick.AddListener(TestGame);

                    PhotonNetwork.NickName = "Player " + Random.Range(10000, 100000);
                    PhotonNetwork.PhotonServerSettings.DevRegion = "ru";
                    PhotonNetwork.PhotonServerSettings.StartInOfflineMode = false;
                    PhotonNetwork.ConnectUsingSettings();
                    PhotonNetwork.GameVersion = VERSION_PHOTON_GAME;
                    break;

                case SceneTypes.Game:
                    break;

                default:
                    break;
            }
        }


        public void CreateRoom()
        {
            Instance.GameModeType = GameModTypes.Multiplayer;
            PhotonNetwork.PhotonServerSettings.StartInOfflineMode = false;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = MAX_PLAYERS;
            //roomOptions.PlayerTtl = 200;//1000
            roomOptions.IsVisible = true;
            roomOptions.IsOpen = true;
            var roomName = Random.Range(1, 9999999).ToString();

            PhotonNetwork.CreateRoom(roomName, roomOptions, null);
        }

        public void JoinRandomRoom()
        {
            Instance.GameModeType = GameModTypes.Multiplayer;
            PhotonNetwork.PhotonServerSettings.StartInOfflineMode = false;

            PhotonNetwork.JoinRandomRoom();
        }

        private void TestGame()
        {
            Instance.GameModeType = GameModTypes.WithBot;
            PhotonNetwork.PhotonServerSettings.StartInOfflineMode = true;

            Instance.ToggleScene(SceneTypes.Game);
        }
        internal void SetTextOnUpLog(string message) => _logTex.text = message;


        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log(message);
            PhotonNetwork.PhotonServerSettings.StartInOfflineMode = true;
        }

        public override void OnJoinedRoom()
        {
            Instance.ToggleScene(SceneTypes.Game);
        }

        public override void OnConnectedToMaster()
        {
            SetTextOnUpLog("Connected to Master");
            PhotonNetwork.ConnectToRegion("ru");
        }

        public override void OnDisconnected(DisconnectCause cause)
        {

        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();

            Instance.ToggleScene(SceneTypes.Menu);
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            base.OnMasterClientSwitched(newMasterClient);

            LeaveRoom();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {

        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            LeaveRoom();
            ToggleScene(SceneTypes.Menu);
        }

        #endregion
    }
}