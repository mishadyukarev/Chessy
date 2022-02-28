using Chessy.Common;
using Photon.Pun;

namespace Chessy.Game
{
    sealed class UpEventUIS
    {
        internal UpEventUIS(in EntitiesModel ents, in EntitiesViewUI entsUI)
        {
            entsUI.UpEs.AlphaC.AddListener(OpenShop);
            entsUI.UpEs.LeaveC.AddListener(delegate { PhotonNetwork.LeaveRoom(); });
        }

        void OpenShop()
        {
            ShopUIC.EnableZone();
        }
    }
}