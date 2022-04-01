using Chessy.Menu.View.UI;
using Photon.Pun;

namespace Chessy.Menu
{
    public struct ConnectorMenuS
    {
        public void Run(in EntitiesViewUIMenu eUIM)
        {
            if (PhotonNetwork.IsConnected)
            {
                if (PhotonNetwork.OfflineMode)
                {
                    BackgroundUIC.SetActiveFrontImage(true);
                    ConnectorUIC.SetActive_Button(true);
                    BackgroundUIC.SetActiveFrontImage(true);
                    
                    eUIM.CenterE.LogTextC.TextUI.text = "Offline";
                    ConUIC.SetActive_Button(false);
                    eUIM.BackE.FrontImageC.GameObject.SetActive(false);
                }
                else if (PhotonNetwork.IsConnectedAndReady)
                {
                    ConUIC.SetActive_Button(true);
                    eUIM.BackE.FrontImageC.GameObject.SetActive(true);

                    eUIM.CenterE.LogTextC.TextUI.text = "Online";
                    BackgroundUIC.SetActiveFrontImage(false);
                    ConnectorUIC.SetActive_Button(false);
                    BackgroundUIC.SetActiveFrontImage(false);
                }
            }
        }
    }
}
