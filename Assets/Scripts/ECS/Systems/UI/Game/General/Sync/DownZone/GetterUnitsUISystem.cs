using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class GetterUnitsUISystem : IEcsRunSystem
{
    private EcsFilter<UnitsInGameInfoComponent> _xyUnitsFilter = default;
    private EcsFilter<TakerUnitsViewUICom> _takerUnitsUIFilter = default;

    public void Run()
    {
        ref var xyUnitsCom = ref _xyUnitsFilter.Get1(0);

        if (xyUnitsCom.IsSettedKing(PhotonNetwork.IsMasterClient))
            _takerUnitsUIFilter.Get1(0).SetActiveButton(UnitTypes.King, false);
        else _takerUnitsUIFilter.Get1(0).SetActiveButton(UnitTypes.King, true);
    }
}
