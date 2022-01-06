using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

namespace Game.Game
{
    public sealed class ReadyZoneUISystem : IEcsRunSystem
    {
        public void Run()
        {
            ref var readyBut = ref EntityUIPool.ReadyCenter<ButtonC>();

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