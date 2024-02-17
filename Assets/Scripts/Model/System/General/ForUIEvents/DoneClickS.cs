using Photon.Pun;
namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void DoneReadyClick()
        {
            rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.TryExecuteDoneReadyM), });
        }
    }
}