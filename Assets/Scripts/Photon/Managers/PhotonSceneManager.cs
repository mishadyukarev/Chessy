﻿using Photon.Pun;
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

        internal void Constructor()
        {

        }

        internal void ToggleScene(SceneTypes sceneType)
        {
            switch (sceneType)
            {
                case SceneTypes.Menu:
                    _logTex = Instance.CanvasManager.FindUnderParent<TextMeshProUGUI>(SceneTypes.Menu, "LogText");

                    Instance.CanvasManager.FindUnderParent<Button>(SceneTypes.Menu, "CreateRoomButton").onClick.AddListener(CreateRoom);
                    Instance.CanvasManager.FindUnderParent<Button>(SceneTypes.Menu, "JoinRandomButton").onClick.AddListener(JoinRandomRoom);
                    Instance.CanvasManager.FindUnderParent<Button>(SceneTypes.Menu, "QuitButton").onClick.AddListener(Quit);

                    PhotonNetwork.NickName = "Player " + Random.Range(10000, 100000);
                    PhotonNetwork.GameVersion = "1";
                    PhotonNetwork.PhotonServerSettings.DevRegion = "ru";
                    PhotonNetwork.PhotonServerSettings.StartInOfflineMode = Instance.IsOfflineMode;
                    PhotonNetwork.ConnectUsingSettings();
                    break;

                case SceneTypes.Game:
                    break;

                default:
                    break;
            }
        }


        public void CreateRoom()
        {
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
            PhotonNetwork.JoinRandomRoom();
        }

        internal void Log(string message)
        {
            //Debug.Log(message);
            //_logTex.text += "\n";
            _logTex.text += message;
        }

        public void Quit() => Application.Quit();







        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log(message);
        }

        public override void OnJoinedRoom()
        {
            Instance.ToggleScene(SceneTypes.Game);
        }

        public override void OnConnectedToMaster()
        {
            Log("Connected to Master");
        }

        #endregion














        internal void OwnUpdate()
        {

        }



        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        internal void Disconect() => PhotonNetwork.Disconnect();

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
        }
    }
}