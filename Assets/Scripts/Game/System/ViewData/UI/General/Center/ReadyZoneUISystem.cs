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
                ReadyUIC.SetColorReadyButton(Color.red);
            }
            else
            {
                ReadyUIC.SetColorReadyButton(Color.white);
            }

            if (ReadyC.IsStartedGame || PhotonNetwork.OfflineMode)
            {
                ReadyUIC.SetActiveParent(false);
            }
            else
            {
                ReadyUIC.SetActiveParent(true);
            }
        }
    }
}