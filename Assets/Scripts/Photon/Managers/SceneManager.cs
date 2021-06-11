using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Main;

internal sealed class SceneManager : MonoBehaviourPunCallbacks
{
    #region Menu

    private TextMeshProUGUI _logTex;
    private const int MAX_PLAYERS = 2;

    internal void InitMenu()
    {

        _logTex = Instance.CanvasManager.InMenuZoneCanvasGO.transform.Find("LogText").GetComponent<TextMeshProUGUI>();

        Instance.CanvasManager.InMenuZoneCanvasGO.transform.Find("CreateRoomButton").GetComponent<Button>().onClick.AddListener(CreateRoom);
        Instance.CanvasManager.InMenuZoneCanvasGO.transform.Find("JoinRandomButton").GetComponent<Button>().onClick.AddListener(JoinRandomRoom);
        Instance.CanvasManager.InMenuZoneCanvasGO.transform.Find("QuitButton").GetComponent<Button>().onClick.AddListener(Quit);


        PhotonNetwork.NickName = "Player " + Random.Range(10000, 100000);
        Log("Player's name is set to " + PhotonNetwork.NickName);

        PhotonNetwork.GameVersion = "100";
        PhotonNetwork.ConnectUsingSettings();
    }


    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = MAX_PLAYERS;
        roomOptions.PlayerTtl = 200;//1000
        var roomName = Random.Range(10000, 100000).ToString();

        PhotonNetwork.CreateRoom(roomName, roomOptions, null);
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
        //Instance.ToggleScene(SceneTypes.Menu);
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
    }
}
