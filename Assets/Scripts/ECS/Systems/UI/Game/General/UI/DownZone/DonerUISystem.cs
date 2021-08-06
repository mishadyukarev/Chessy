using Assets.Scripts.Workers.Game.UI;
using Assets.Scripts.Workers.Game.UI.Middle;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

internal sealed class DonerUISystem : IEcsInitSystem, IEcsRunSystem
{
    public void Init()
    {
        DownDonerUIDataContainer.AddListener(MistakeDone);
    }

    public void Run()
    {
        if (DownDonerUIDataContainer.IsDoned(PhotonNetwork.IsMasterClient))
            DonerUIWorker.SetColor(Color.red);
        else DonerUIWorker.SetColor(Color.white);
    }

    private void MistakeDone()
    {
        DownGetterUnitsUIWorker.SetColorKing(Color.red);
    }
}
