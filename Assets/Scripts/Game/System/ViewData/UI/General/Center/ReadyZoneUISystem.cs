using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class ReadyZoneUISystem : IEcsRunSystem
    {
        public void Run()
        {
            if (ReadyDataUIC.IsReady(WhoseMoveC.CurPlayerI))
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