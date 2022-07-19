using Photon.Pun;
using UnityEngine;
namespace Chessy.Model.System
{
    public sealed class Rpc : MonoBehaviour
    {
        SystemsModel _s;
        public static string NameRpcMethod => nameof(PunRPC);

        public Rpc Fill(in SystemsModel sM) { _s = sM; return this; }
        [PunRPC] void PunRPC(object[] objects, PhotonMessageInfo infoFrom) => _s.RpcSs.PunRPC(objects, infoFrom);

    }

    public sealed class PhotonSerializeView : MonoBehaviour, IPunObservable
    {
        SystemsModel _s;
        SyncTypes _curSyncType;

        public PhotonSerializeView Fill(in SyncTypes syncType, in SystemsModel sM)
        {
            _s = sM;
            _curSyncType = syncType;
            return this;
        }
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) => _s.OnPhotonSerializeViewS.OnPhotonSerializeView0(_curSyncType, stream, info);
    }
}