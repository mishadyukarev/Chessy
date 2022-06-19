using Photon.Pun;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        public void DoneReadyClick()
        {
            _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, RpcTarget.MasterClient, new object[] { nameof(_sMG.TryDoneM), });
        }
    }
}