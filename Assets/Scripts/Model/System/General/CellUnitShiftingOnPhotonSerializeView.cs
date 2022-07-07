//using Chessy.Model.Entity;
//using Photon.Pun;
//using UnityEngine;

//namespace Chessy.Model.System
//{
//    public sealed class CellUnitShiftingOnPhotonSerializeView : MonoBehaviour, IPunObservable
//    {
//        EntitiesModel _e;
//        byte _cellIdx;

//        public void GiveData(in byte cellIdx, in EntitiesModel eM)
//        {
//            _cellIdx = cellIdx;
//            _e = eM;
//        }
//        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
//        {

//        }
//    }
//}