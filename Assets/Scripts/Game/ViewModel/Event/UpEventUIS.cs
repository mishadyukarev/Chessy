using Chessy.Common;
using Photon.Pun;

namespace Chessy.Game
{
    sealed class UpEventUIS
    {
        internal UpEventUIS( in EntitiesViewUI entsUI, in EntitiesModel ents)
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