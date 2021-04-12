using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonManagerScene : MonoBehaviourPunCallbacks
{

    public void Leave()
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

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat($"Player {0} entered room", newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat($"Player {0} left room", otherPlayer.NickName);
    }

    //public override void OnMasterClientSwitched(Player newMasterClient)
    //{
    //    //if (PhotonNetwork.IsMasterClient)
    //    //{
    //    //    Instance.SetEntityAndSystemNet();
    //    //}
    //}

}
