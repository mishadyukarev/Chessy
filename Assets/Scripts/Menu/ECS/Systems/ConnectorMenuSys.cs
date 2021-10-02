﻿using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Menu;
using Assets.Scripts.ECS.Component.UI;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Menu
{
    internal sealed class ConnectorMenuSys : IEcsRunSystem
    {
        private EcsFilter<ConnectButtonUICom, OnlineZoneUICom, BackgroundMenuUICom> _rightZoneFilter = default;
        private EcsFilter<ConnectButtonUICom, OfflineZoneUICom, BackgroundMenuUICom> _leftZoneFilter = default;
        private EcsFilter<CenterMenuUICom> _centerUIZoneFilter = default;

        public void Run()
        {
            ref var rightConnectCom = ref _rightZoneFilter.Get1(0);
            ref var rightOnlineCom = ref _rightZoneFilter.Get2(0);
            ref var rightBackCom = ref _rightZoneFilter.Get3(0);

            ref var leftConnectCom = ref _leftZoneFilter.Get1(0);
            ref var leftBackCom = ref _leftZoneFilter.Get3(0);

            if (PhotonNetwork.connected)
            {
                if (PhotonNetwork.offlineMode)
                {
                    rightBackCom.SetActiveFrontImage(true);
                    rightConnectCom.SetActive_Button(true);
                    rightBackCom.SetActiveFrontImage(true);

                    _centerUIZoneFilter.Get1(0).SetLogText("Offline");
                    leftConnectCom.SetActive_Button(false);
                    leftBackCom.SetActiveFrontImage(false);
                }
                else
                {
                    leftConnectCom.SetActive_Button(true);
                    leftBackCom.SetActiveFrontImage(true);

                    _centerUIZoneFilter.Get1(0).SetLogText("Online");
                    rightBackCom.SetActiveFrontImage(false);
                    rightConnectCom.SetActive_Button(false);
                    rightBackCom.SetActiveFrontImage(false);
                }
            }
        }
    }
}