//using Photon.Pun;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerControls : MonoBehaviour, IPunObservable
//{
//    private PhotonView _photonView;
//    private SpriteRenderer _spriteRenderer;

//    private Vector2Int _direction;

//    private bool _isRed;

//    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
//    {
//        // bool
//        //if (stream.IsWriting)
//        //{
//        //    stream.SendNext(_isRed);
//        //}
//        //else
//        //{
//        //    _isRed = (bool) stream.ReceiveNext();
//        //}

//        if (stream.IsWriting)
//        {
//            stream.SendNext(_direction);
//        }
//        else
//        {
//            _direction = (Vector2Int)stream.ReceiveNext();
//        }
//    }

//    void Start()
//    {
//        _photonView = GetComponent<PhotonView>();
//        _spriteRenderer = GetComponent<SpriteRenderer>();
//    }

//    void Update()
//    {
//        if (_photonView.IsMine)
//        {
//            if (Input.GetKey(KeyCode.RightArrow)) _direction = Vector2Int.right;
//            if (Input.GetKey(KeyCode.LeftArrow)) _direction = Vector2Int.left;
//            if (Input.GetKey(KeyCode.UpArrow)) _direction = Vector2Int.up;
//            if (Input.GetKey(KeyCode.DownArrow)) _direction = Vector2Int.down;
//        }

//        if (_direction == Vector2Int.left) _spriteRenderer.flipX = false;
//        if (_direction == Vector2Int.right) _spriteRenderer.flipX = true;
//    }
//}
