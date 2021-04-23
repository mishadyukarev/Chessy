using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

internal class MenuManager : MonoBehaviourPunCallbacks
{
    private TextMeshProUGUI _logTex;
    private const int MAX_PLAYERS = 2;

    internal void Init(StartSpawnMenuManager startSpawnMenuManager)
    {
        _logTex = startSpawnMenuManager.LogText;
        startSpawnMenuManager.CreateRoomButton.onClick.AddListener(CreateRoom);
        startSpawnMenuManager.JoinRandomButton.onClick.AddListener(JoinRandomRoom);
        startSpawnMenuManager.QuitButton.onClick.AddListener(Quit);


        PhotonNetwork.NickName = "Player " + Random.Range(1000, 9999);
        Log("Player's name is set to " + PhotonNetwork.NickName);

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }


    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = MAX_PLAYERS;
        PhotonNetwork.CreateRoom(null, roomOptions, null);
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    private void Log(string message)
    {
        Debug.Log(message);
        _logTex.text += "\n";
        _logTex.text += message;
    }

    public void Quit() => Application.Quit();







    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("There isn't rooms");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Chessi");
    }

    public override void OnConnectedToMaster()
    {
        Log("Connected to Master");
    }

}
