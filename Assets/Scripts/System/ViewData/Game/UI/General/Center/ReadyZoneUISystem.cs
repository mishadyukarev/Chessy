using Photon.Pun;
using UnityEngine;
using static Game.Game.EntityCenterUIPool;

namespace Game.Game
{
    struct ReadyZoneUISystem : IEcsRunSystem
    {
        public void Run()
        {
            ref var readyBut = ref Ready<ButtonVC>();

            readyBut.Color = ReadyC.IsReady(WhoseMoveC.CurPlayerI) ? Color.red : Color.white;

            if (ReadyC.IsStartedGame || PhotonNetwork.OfflineMode)
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