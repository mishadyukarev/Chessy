using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

namespace Game.Game
{
    public sealed class ReadyZoneUISystem : IEcsRunSystem
    {
        public void Run()
        {
            if (ReadyC.IsReady(WhoseMoveC.CurPlayerI))
            {
                ReadyViewUIC.SetColorReadyButton(Color.red);
            }
            else
            {
                ReadyViewUIC.SetColorReadyButton(Color.white);
            }

            if (ReadyC.IsStartedGame || PhotonNetwork.OfflineMode)
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