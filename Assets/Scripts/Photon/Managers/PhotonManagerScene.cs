using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonManagerScene : MonoBehaviourPunCallbacks
{

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        // Когда текущий игрок (мы) покидаем комнату
        //base.OnLeftRoom();
        Debug.Log("Вышел");

        SceneManager.LoadScene(0);
    }






    //public override void OnMasterClientSwitched(Player newMasterClient)
    //{
    //    base.OnMasterClientSwitched(newMasterClient);

    //    LeaveRoom();
    //    //SceneManager.LoadScene(0);
    //    //PhotonNetwork.LeaveRoom();
    //}

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat($"Player {0} entered room", newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat($"Player {0} left room", otherPlayer.NickName);

        LeaveRoom();
    }
}
