using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviourPunCallbacks
{
    public Text LogText;
    private const int MAX_PLAYERS = 2;

    public void Start()
    {
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
        LogText.text += "\n";
        LogText.text += message;
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
