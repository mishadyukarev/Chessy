using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

internal sealed class ReadyZoneUISystem : IEcsRunSystem
{
    public void Run()
    {
        if (MiddleViewUIWorker.IsReady(PhotonNetwork.IsMasterClient))
        {
            ReadyZoneUIWorker.SetColorButton(Color.red);
        }
        else
        {
            ReadyZoneUIWorker.SetColorButton(Color.white);
        }

        if (ReadyZoneUIWorker.IsStartedGame || PhotonNetwork.OfflineMode)
        {
            ReadyZoneUIWorker.SetActiveParentGO(false);
        }
        else
        {
            ReadyZoneUIWorker.SetActiveParentGO(true);
        }
    }
}