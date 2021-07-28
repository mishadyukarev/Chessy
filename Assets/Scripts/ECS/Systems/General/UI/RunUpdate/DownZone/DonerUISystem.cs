using Assets.Scripts.Workers.Game.UI;
using Assets.Scripts.Workers.Game.UI.Middle;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

internal sealed class DonerUISystem : IEcsInitSystem, IEcsRunSystem
{
    public void Init()
    {
        DownDonerUIWorker.AddListener(MistakeDone);
    }

    public void Run()
    {
        if (DownDonerUIWorker.IsDoned(PhotonNetwork.IsMasterClient))
            DonerUIWorker.SetColor(Color.red);
        else DonerUIWorker.SetColor(Color.white);
    }

    private void MistakeDone()
    {
        DownGetterUnitsUIWorker.SetColorKing(Color.red);
    }
}
