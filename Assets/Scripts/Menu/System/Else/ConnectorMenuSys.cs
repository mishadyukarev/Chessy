using Leopotam.Ecs;
using Photon.Pun;

namespace Scripts.Menu
{
    internal sealed class ConnectorMenuSys : IEcsRunSystem
    {
        private EcsFilter<ConnectButtonUICom, OnlineZoneUICom, BackgroundMenuUICom> _rightZoneFilter = default;
        private EcsFilter<ConnectButtonUICom, OfflineZoneUICom, BackgroundMenuUICom> _leftZoneFilter = default;

        public void Run()
        {
            ref var rightConnectCom = ref _rightZoneFilter.Get1(0);
            ref var rightOnlineCom = ref _rightZoneFilter.Get2(0);
            ref var rightBackCom = ref _rightZoneFilter.Get3(0);

            ref var leftConnectCom = ref _leftZoneFilter.Get1(0);
            ref var leftBackCom = ref _leftZoneFilter.Get3(0);

            if (PhotonNetwork.IsConnected)
            {
                if (PhotonNetwork.OfflineMode)
                {
                    rightBackCom.SetActiveFrontImage(true);
                    rightConnectCom.SetActive_Button(true);
                    rightBackCom.SetActiveFrontImage(true);

                    CenterZoneUICom.SetLogText("Offline");
                    leftConnectCom.SetActive_Button(false);
                    leftBackCom.SetActiveFrontImage(false);
                }
                else if (PhotonNetwork.IsConnectedAndReady)
                {
                    leftConnectCom.SetActive_Button(true);
                    leftBackCom.SetActiveFrontImage(true);

                    CenterZoneUICom.SetLogText("Online");
                    rightBackCom.SetActiveFrontImage(false);
                    rightConnectCom.SetActive_Button(false);
                    rightBackCom.SetActiveFrontImage(false);
                }
            }
        }
    }
}
