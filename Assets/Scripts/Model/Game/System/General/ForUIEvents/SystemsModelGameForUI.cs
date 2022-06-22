﻿using Chessy.Game.Model.Entity;
using Photon.Pun;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGameForUI : SystemModel
    {
        internal SystemsModelGameForUI(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        public void TryBuyFromMarketBuilding(in MarketBuyTypes marketBuyT)
        {
            _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TryBuyFromMarketBuildingM), marketBuyT });
        }

        public void ClickFriendReady()
        {
            _e.ZoneInfoC.IsActiveFriend = false;
            _e.NeedUpdateView = true;
        }
    }
}