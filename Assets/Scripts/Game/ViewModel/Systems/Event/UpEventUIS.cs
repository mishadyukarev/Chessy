using Chessy.Common;
using Photon.Pun;

namespace Chessy.Game
{
    sealed class UpEventUIS
    {
        readonly EntitiesModel _e;

        internal UpEventUIS( in EntitiesViewUI entsUI, in EntitiesModel ents)
        {
            _e = ents;

            entsUI.UpEs.AlphaC.AddListener(OpenShop);
            entsUI.UpEs.LeaveC.AddListener(delegate { PhotonNetwork.LeaveRoom(); });
        }

        void OpenShop()
        {
            _e.Sound(ClipTypes.Click).Invoke();
            ShopUIC.EnableZone();
        }
    }
}