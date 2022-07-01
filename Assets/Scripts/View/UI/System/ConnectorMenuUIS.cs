using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.View.UI.Entity;
using Photon.Pun;

namespace Chessy.View.UI.System
{
    sealed class ConnectorMenuUIS : SystemUIAbstract
    {
        readonly EntitiesViewUI _eUI;

        internal ConnectorMenuUIS(in EntitiesViewUI eV, in EntitiesModel eMG) : base(eMG)
        {
            _eUI = eV;
        }

        internal override void Sync()
        {
            if (PhotonNetwork.IsConnected)
            {
                if (PhotonNetwork.OfflineMode)
                {
                    _eUI.OfflineZoneE.JoinButtonC.SetActive(false);
                    _eUI.OfflineZoneE.FrontImageC.SetActive(false);

                    _eUI.OnlineZoneE.JoinButtonC.SetActive(true);
                    _eUI.OnlineZoneE.FronImageC.SetActive(true);

                    _eUI.CenterE.LogTextC.TextUI.text = "Offline";
                }
                else if (PhotonNetwork.IsConnectedAndReady)
                {
                    _eUI.OnlineZoneE.JoinButtonC.SetActive(false);
                    _eUI.OnlineZoneE.FronImageC.SetActive(false);

                    _eUI.OfflineZoneE.JoinButtonC.SetActive(true);
                    _eUI.OfflineZoneE.FrontImageC.SetActive(true);

                    _eUI.CenterE.LogTextC.TextUI.text = "Online";
                }
            }
        }
    }
}
