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
    private const byte MAX_PLAYERS = 2;
    private LoadBalancingClient _loadBalancingClient = new LoadBalancingClient();

    internal void ToggleScene(SceneTypes sceneType)
    {
        switch (sceneType)
        {
            case SceneTypes.Menu:
                _logTex = Instance.CanvasManager.InMenuZoneCanvasGO.transform.Find("LogText").GetComponent<TextMeshProUGUI>();

                Instance.CanvasManager.InMenuZoneCanvasGO.transform.Find("CreateRoomButton").GetComponent<Button>().onClick.AddListener(CreateRoom);
                Instance.CanvasManager.InMenuZoneCanvasGO.transform.Find("JoinRandomButton").GetComponent<Button>().onClick.AddListener(JoinRandomRoom);
                Instance.CanvasManager.InMenuZoneCanvasGO.transform.Find("QuitButton").GetComponent<Button>().onClick.AddListener(Quit);


                //_loadBalancingClient.NickName = "Player " + Random.Range(10000, 100000);
                //Log("Player's name is set to " + _loadBalancingClient.NickName);
                //_loadBalancingClient.AppVersion = "1";
                //_loadBalancingClient.ConnectUsingSettings(new AppSettings());

                PhotonNetwork.NickName = "Player " + Random.Range(10000, 100000);
                Log("Player's name is set to " + PhotonNetwork.NickName);
                PhotonNetwork.GameVersion = "1";
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
        //EnterRoomParams enterRoomParams = new EnterRoomParams();
        //enterRoomParams.RoomOptions = new RoomOptions();
        //enterRoomParams.RoomName = Random.Range(1, 9999999999).ToString();
        //enterRoomParams.RoomOptions.MaxPlayers = MAX_PLAYERS;
        //_loadBalancingClient.OpCreateRoom(enterRoomParams);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = MAX_PLAYERS;
        roomOptions.PlayerTtl = 200;//1000
        var roomName = Random.Range(1, 9999999).ToString();

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
