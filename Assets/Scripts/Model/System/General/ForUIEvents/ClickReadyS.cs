using Photon.Pun;
namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void ClickReady()
        {
            rpcC.Rpc(RpcTarget.MasterClient, new object[] { nameof(s.TryExecuteReadyForOnlineM) });

            updateAllViewC.NeedUpdateView = true;
        }
    }
}