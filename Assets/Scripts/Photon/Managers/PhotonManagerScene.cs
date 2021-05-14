using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonManagerScene : MonoBehaviourPunCallbacks
{

    public void LeaveRoom() => PhotonNetwork.LeaveRoom();
    public void LoadScene(int number) => SceneManager.LoadScene(number);







    public override void OnLeftRoom()
    {
        base.OnLeftRoom();

        Debug.Log("Вышел");
        LoadScene(0);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        base.OnMasterClientSwitched(newMasterClient);

        LoadScene(0);
        LeaveRoom();
    }

    //public override void OnPlayerEnteredRoom(Player newPlayer)
    //{
    //    Debug.LogFormat($"Player {0} entered room", newPlayer.NickName);
    //}

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //Debug.LogFormat($"Player {0} left room", otherPlayer.NickName);

        LoadScene(0);
        LeaveRoom();
    }
}
