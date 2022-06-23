using Chessy.Model;
using Photon.Pun;

namespace Chessy.Menu
{
    sealed class ConnectorMenuUIS
    {
        public void Run(in EntitiesViewUI eUI)
        {
            if (PhotonNetwork.IsConnected)
            {
                if (PhotonNetwork.OfflineMode)
                {
                    eUI.OfflineZoneE.JoinButtonC.SetActive(false);
                    eUI.OfflineZoneE.FrontImageC.SetActive(false);

                    eUI.OnlineZoneE.JoinButtonC.SetActive(true);
                    eUI.OnlineZoneE.FronImageC.SetActive(true);

                    eUI.CenterE.LogTextC.TextUI.text = "Offline";
                }
                else if (PhotonNetwork.IsConnectedAndReady)
                {
                    eUI.OnlineZoneE.JoinButtonC.SetActive(false);
                    eUI.OnlineZoneE.FronImageC.SetActive(false);

                    eUI.OfflineZoneE.JoinButtonC.SetActive(true);
                    eUI.OfflineZoneE.FrontImageC.SetActive(true);

                    eUI.CenterE.LogTextC.TextUI.text = "Online";
                }
            }
        }
    }
}
