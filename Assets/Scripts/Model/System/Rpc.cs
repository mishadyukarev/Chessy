using Photon.Pun;
using UnityEngine;
namespace Chessy.Model.System
{
    public sealed class Rpc : MonoBehaviour
    {
        SystemsModel _s;
        public static string NameRpcMethod => nameof(PunRPC);
        public Rpc FillRpcWithSystems(in SystemsModel sM) { _s = sM; return this; }
        [PunRPC] void PunRPC(object[] objects, PhotonMessageInfo infoFrom) => _s.RpcSs.PunRPC(objects, infoFrom);
    }
}