using Photon.Pun;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        public void DoneReadyClick()
        {
            _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TryExecuteDoneReadyM), });
        }
    }
}