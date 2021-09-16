using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class RightZoneUISystem : IEcsRunSystem
{
    private EcsFilter<SelectorComponent> _selectorFilter = default;
    private EcsFilter<UnitZoneViewUICom> _unitZoneFilter = default;

    private EcsFilter<CellUnitDataComponent, OwnerOnlineComp, OwnerBotComponent> _cellUnitFilter = default;

    public void Run()
    {
        var idxSelectedCell = _selectorFilter.Get1(0).IdxSelectedCell;
        ref var unitZoneViewUICom = ref _unitZoneFilter.Get1(0);

        ref var selCellUnitDataCom = ref _cellUnitFilter.Get1(idxSelectedCell);
        ref var selOwnerCellUnitCom = ref _cellUnitFilter.Get2(idxSelectedCell);
        ref var selBotOwnerCellUnitCom = ref _cellUnitFilter.Get3(idxSelectedCell);


        if (_selectorFilter.Get1(0).IsSelectedCell)
        {
            if (selCellUnitDataCom.IsVisibleUnit(PhotonNetwork.IsMasterClient))
            {
                if (selCellUnitDataCom.HaveUnit)
                {
                    if (selOwnerCellUnitCom.HaveOwner)
                    {
                        unitZoneViewUICom.SetActiveParentZone(true);
                    }
                    else if (selBotOwnerCellUnitCom.IsBot)
                    {
                        unitZoneViewUICom.SetActiveParentZone(true);
                    }
                }
                else unitZoneViewUICom.SetActiveParentZone(false);
            }
            else
            {
                unitZoneViewUICom.SetActiveParentZone(false);
            }
        }
        else
        {
            unitZoneViewUICom.SetActiveParentZone(false);
        }
    }
}
