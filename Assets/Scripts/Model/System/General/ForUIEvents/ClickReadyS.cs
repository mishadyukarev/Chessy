using Photon.Pun;
namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void ClickReady()
        {
            _rpcC.Rpc(RpcTarget.MasterClient, new object[] { nameof(_s.TryExecuteReadyForOnlineM) });

            _updateAllViewC.NeedUpdateView = true;
        }
    }
}