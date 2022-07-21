using Photon.Pun;
namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void DoneReadyClick()
        {
            _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TryExecuteDoneReadyM), });
        }
    }
}