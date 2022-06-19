using Chessy.Game.Model.Entity;
using Photon.Pun;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        readonly EntitiesModelGame _eMG;
        readonly SystemsModelGame _sMG;

        internal SystemsModelGameForUI(in SystemsModelGame sMG, in EntitiesModelGame eMG)
        {
            _eMG = eMG;
            _sMG = sMG;
        }

        public void TryBuyFromMarketBuilding(in MarketBuyTypes marketBuyT)
        {
            _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, RpcTarget.MasterClient, new object[] { nameof(_sMG.TryBuyFromMarketBuildingM), marketBuyT });
        }
    }
}