using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class ReadyZoneUISystem : IEcsRunSystem
    {
        private EcsFilter<ReadyDataUICom, ReadyViewUICom> _readyUIFilter = default;

        public void Run()
        {
            ref var readyDataUICom = ref _readyUIFilter.Get1(0);
            ref var readyViewUICom = ref _readyUIFilter.Get2(0);

            if (readyDataUICom.IsReady(PhotonNetwork.IsMasterClient))
            {
                readyViewUICom.SetColorReadyButton(Color.red);
            }
            else
            {
                readyViewUICom.SetColorReadyButton(Color.white);
            }

            if (readyDataUICom.IsStartedGame || PhotonNetwork.OfflineMode)
            {
                readyViewUICom.SetActiveParent(false);
            }
            else
            {
                readyViewUICom.SetActiveParent(true);
            }
        }
    }
}