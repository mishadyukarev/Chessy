using Assets.Scripts.Workers.Game.Else.Info.Units;
using Assets.Scripts.Workers.Game.UI.Middle;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class GetterUnitsUISystem : IEcsRunSystem
{
    public void Run()
    {
        if (InfoAmountUnitsWorker.IsSettedKing(PhotonNetwork.IsMasterClient))
            DownGetterUnitsUIWorker.SetActiveKingButton(false);
        else DownGetterUnitsUIWorker.SetActiveKingButton(true);
    }
}
