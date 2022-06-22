using Chessy.Common.Entity;
using Chessy.Menu.View.UI;
using Photon.Pun;

namespace Chessy.Menu
{
    sealed class ConnectorMenuUIS
    {
        public void Run(in EntitiesViewUIMenu eUIM)
        {
            if (PhotonNetwork.IsConnected)
            {
                if (PhotonNetwork.OfflineMode)
                {
                    eUIM.OfflineZoneE.JoinButtonC.SetActive(false);
                    eUIM.OfflineZoneE.FrontImageC.SetActive(false);

                    eUIM.OnlineZoneE.JoinButtonC.SetActive(true);
                    eUIM.OnlineZoneE.FronImageC.SetActive(true);
                    
                    eUIM.CenterE.LogTextC.TextUI.text = "Offline";
                }
                else if (PhotonNetwork.IsConnectedAndReady)
                {
                    eUIM.OnlineZoneE.JoinButtonC.SetActive(false);
                    eUIM.OnlineZoneE.FronImageC.SetActive(false);

                    eUIM.OfflineZoneE.JoinButtonC.SetActive(true);
                    eUIM.OfflineZoneE.FrontImageC.SetActive(true);

                    eUIM.CenterE.LogTextC.TextUI.text = "Online";
                }
            }
        }
    }
}
