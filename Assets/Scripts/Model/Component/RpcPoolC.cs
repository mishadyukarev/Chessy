using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
namespace Chessy.Model
{
    public sealed class RpcPoolC
    {
        internal readonly ActionMy<string, RpcTarget, object[]> Action0;
        internal readonly ActionMy<string, Player, object[]> Action1;

        internal readonly string PunRPCName;

        internal RpcPoolC(in List<object> actions, in string name)
        {
            var idx = 0;

            Action0 = (ActionMy<string, RpcTarget, object[]>)actions[idx++];
            Action1 = (ActionMy<string, Player, object[]>)actions[idx++];

            PunRPCName = name;
        }

        internal void Rpc(in RpcTarget rpcTarget, in object[] objs) => Action0(PunRPCName, rpcTarget, objs);
        internal void Rpc(in Player player, in object[] objs) => Action1(PunRPCName, player, objs);
    }
}
