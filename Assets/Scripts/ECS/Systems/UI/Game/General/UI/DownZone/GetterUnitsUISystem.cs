using Assets.Scripts.ECS.Component;
using Assets.Scripts.Workers.Game.UI.Middle;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class GetterUnitsUISystem : IEcsRunSystem
{
    private EcsFilter<XyUnitsComponent> _xyUnitsFilter;

    public void Run()
    {
        ref var xyUnitsCom = ref _xyUnitsFilter.Get1(0);

        if (xyUnitsCom.IsSettedKing(PhotonNetwork.IsMasterClient))
            DownGetterUnitsUIWorker.SetActiveKingButton(false);
        else DownGetterUnitsUIWorker.SetActiveKingButton(true);
    }
}
