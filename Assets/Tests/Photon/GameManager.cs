//using ExitGames.Client.Photon;
//using Photon;
//using Photon.Pun;
//using Photon.Realtime;
//using System;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class GameManager : MonoBehaviourPunCallbacks
//{
//    public GameObject PlayerPrefab;

//    void Start()
//    {
//        Vector3 pos = new Vector3(UnityEngine.Random.Range(-5f, 5f), (UnityEngine.Random.Range(-5f, 5f)));
//        PhotonNetwork.Instantiate(PlayerPrefab.name, pos, Quaternion.identity);

//        PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializeVector2Int, DeserializeVector2Int);
//    }

//    void Update()
//    {

//    }

//    public void Leave()
//    {
//        PhotonNetwork.LeaveRoom();
//    }

//    public override void OnLeftRoom()
//    {
//        // Когда текущий игрок (мы) покидаем комнату
//        //base.OnLeftRoom();

//        SceneManager.LoadScene(0);
//    }

//    public override void OnPlayerEnteredRoom(Player newPlayer)
//    {
//        //base.OnPlayerEnteredRoom(newPlayer);
//        Debug.LogFormat($"Player {0} entered room", newPlayer.NickName);
//    }

//    public override void OnPlayerLeftRoom(Player otherPlayer)
//    {
//        //base.OnPlayerLeftRoom(otherPlayer);
//        Debug.LogFormat($"Player {0} left room", otherPlayer.NickName);

//    }


//    public static object DeserializeVector2Int(byte[] data)
//    {
//        Vector2Int result = new Vector2Int();

//        result.x = BitConverter.ToInt32(data, 0);
//        result.y = BitConverter.ToInt32(data, 4);

//        return result;

//    }
//    public static byte[] SerializeVector2Int(object obj)
//    {
//        Vector2Int vector = (Vector2Int)obj;
//        byte[] result = new byte[8];

//        BitConverter.GetBytes(vector.x).CopyTo(result, 0);
//        BitConverter.GetBytes(vector.y).CopyTo(result, 4);

//        return result;
//    }
//}
