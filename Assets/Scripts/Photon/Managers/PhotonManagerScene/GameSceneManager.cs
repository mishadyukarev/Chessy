﻿using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

internal class GameSceneManager : SceneManager
{
    private float _timer;
    private Player _exitedPlayer;

    internal void OwnUpdate()
    {
        if (_exitedPlayer != default)
        {
            if (_exitedPlayer.IsInactive)
            {
                _timer += Time.deltaTime;
                if (_timer >= 10)
                {
                    LoadScene(0);
                    LeaveRoom();
                }
            }
            else
            {
                _timer = 0;
            }
        }
    }



    public void LeaveRoom() => PhotonNetwork.LeaveRoom();
    public void LoadScene(int number) => UnityEngine.SceneManagement.SceneManager.LoadScene(number);

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

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //Debug.LogFormat($"Player {0} entered room", newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //Debug.LogFormat($"Player {0} left room", otherPlayer.NickName);

        _exitedPlayer = otherPlayer;
    }
}
