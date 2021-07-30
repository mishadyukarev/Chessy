using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;
using static Assets.Scripts.Main;

internal sealed class ReadyZoneUISystem : IEcsRunSystem
{
    public void Run()
    {
        if (MiddleVisUIWorker.IsReady(Instance.IsMasterClient))
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