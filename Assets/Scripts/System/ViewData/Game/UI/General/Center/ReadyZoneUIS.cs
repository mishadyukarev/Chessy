using Photon.Pun;
using UnityEngine;
using static Game.Game.EntityCenterUIPool;

namespace Game.Game
{
    struct ReadyZoneUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var readyBut = ref Ready<ButtonUIC>();

            readyBut.Color = Entities.Ready(Entities.WhoseMove.CurPlayerI).IsReadyC.IsReady ? Color.red : Color.white;

            if (Entities.GameInfo.IsStartedGameC.IsStartedGame || PhotonNetwork.OfflineMode)
            {
                readyBut.SetActiveParent(false);
            }
            else
            {
                readyBut.SetActiveParent(true);
            }
        }
    }
}