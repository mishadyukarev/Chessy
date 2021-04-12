using UnityEngine;

namespace Assets.Scripts.Tests
{
    public class Test : MonoBehaviour
    {
        private GameObject[,] _gameObjects;
        private void Start()
        {
            _gameObjects = new GameObject[3, 5];

            Debug.Log(_gameObjects[1, 0]);


            //for (int i = 0; i < rows; i++)
            //{
            //    for (int j = 0; i < columns; i++)
            //    {
            //        Debug.Log($"{_gameObjects[i, j]} \t");
            //    }
            //}

            //int[,] mas = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 }, { 10, 11, 12 },{ 3,3,3} };

            //int rows = mas.GetUpperBound(0) + 1;
            //int columns = mas.Length / rows;
            //// или так
            //// int columns = mas.GetUpperBound(1) + 1;

            //for (int i = 0; i < rows; i++)
            //{
            //    for (int j = 0; j < columns; j++)
            //    {
            //        Debug.Log($"{mas[i, j]} \t");
            //    }
            //    Debug.Log("____________");
            //}

        }

    }

    #region Events

    //class Test : MonoBehaviour, IOnEventCallback
    //{

    //    private GameObject _gameObject;

    //    private void Start()
    //    {
    //        PhotonNetwork.AddCallbackTarget(this);

    //        _gameObject = Resources.Load("TestGO")as GameObject;
    //    }

    //    private void Update()
    //    {
    //        if (PhotonNetwork.IsMasterClient)
    //        {
    //            if (Input.GetKeyDown(KeyCode.A))
    //            {
    //                SpawnPlayer();
    //            }
    //        }
    //    }

    //    public void SpawnPlayer()
    //    {
    //        GameObject player = Instantiate(_gameObject);
    //        PhotonView photonView = player.GetComponent<PhotonView>();

    //        if (PhotonNetwork.AllocateViewID(photonView))
    //        {
    //            object[] data = new object[]
    //            {
    //                player.transform.position, player.transform.rotation, photonView.ViewID
    //            };

    //            RaiseEventOptions raiseEventOptions = new RaiseEventOptions
    //            {
    //                Receivers = ReceiverGroup.Others,
    //                CachingOption = EventCaching.AddToRoomCache
    //            };

    //            SendOptions sendOptions = new SendOptions
    //            {
    //                Reliability = true
    //            };

    //            PhotonNetwork.RaiseEvent(12, data, raiseEventOptions, sendOptions);
    //        }
    //        else
    //        {
    //            Debug.LogError("Failed to allocate a ViewId.");

    //            Destroy(player);
    //        }        
    //    }


    //    public void OnEvent(EventData photonEvent)
    //    {

    //        Debug.Log("OnEvent");
    //        if (photonEvent.Code == 12)
    //        {
    //            object[] data = (object[])photonEvent.CustomData;

    //            GameObject player = (GameObject)Instantiate(_gameObject, (Vector3)data[0], (Quaternion)data[1]);
    //            PhotonView photonView = player.GetComponent<PhotonView>();
    //            photonView.ViewID = (int)data[2];
    //        }
    //    }
    //}

    #endregion
}
