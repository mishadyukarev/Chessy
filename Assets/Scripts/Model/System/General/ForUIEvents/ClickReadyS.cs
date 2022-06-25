﻿using Photon.Pun;

namespace Chessy.Model
{
    public sealed partial class SystemsModelGameForUI
    {
        public void ClickReady()
        {
            _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TryExecuteReadyForOnlineM) });

            _e.NeedUpdateView = true;
        }
    }
}