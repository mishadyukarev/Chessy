using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class ReadyZoneUISystem : IEcsRunSystem
    {
        public void Run()
        {
            if (ReadyDataUIC.IsReady(PhotonNetwork.IsMasterClient))
            {
                ReadyViewUIC.SetColorReadyButton(Color.red);
            }
            else
            {
                ReadyViewUIC.SetColorReadyButton(Color.white);
            }

            if (ReadyDataUIC.IsStartedGame || PhotonNetwork.OfflineMode)
            {
                ReadyViewUIC.SetActiveParent(false);
            }
            else
            {
                ReadyViewUIC.SetActiveParent(true);
            }
        }
    }
}