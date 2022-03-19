using Photon.Pun;

namespace Chessy.Menu
{
    public struct ConnectorMenuS
    {
        public void Run()
        {
            if (PhotonNetwork.IsConnected)
            {
                if (PhotonNetwork.OfflineMode)
                {
                    BackgroundUIC.SetActiveFrontImage(true);
                    ConnectorUIC.SetActive_Button(true);
                    BackgroundUIC.SetActiveFrontImage(true);

                    CenterZoneUICom.SetLogText("Offline");
                    ConUIC.SetActive_Button(false);
                    BackUIC.SetActiveFrontImage(false);
                }
                else if (PhotonNetwork.IsConnectedAndReady)
                {
                    ConUIC.SetActive_Button(true);
                    BackUIC.SetActiveFrontImage(true);

                    CenterZoneUICom.SetLogText("Online");
                    BackgroundUIC.SetActiveFrontImage(false);
                    ConnectorUIC.SetActive_Button(false);
                    BackgroundUIC.SetActiveFrontImage(false);
                }
            }
        }
    }
}
