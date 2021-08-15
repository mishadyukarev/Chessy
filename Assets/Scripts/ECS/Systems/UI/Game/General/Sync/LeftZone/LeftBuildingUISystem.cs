using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Leopotam.Ecs;

internal sealed class LeftBuildingUISystem : IEcsRunSystem
{
    private EcsFilter<SelectorComponent> _selectorFilter = default;
    private EcsFilter<DonerDataUIComponent, DonerViewUIComponent> _donerUIFilter = default;
    private EcsFilter<BuildLeftZoneViewUICom> _buildZoneUIFilter = default;
    private EcsFilter<GetterUnitsDataUICom, GetterUnitsViewUICom> _takerUIFilter = default;
    private EcsFilter<CellBuildDataComponent, OwnerComponent> _cellBuildFilter = default;

    public void Run()
    {
        ref var selCom = ref _selectorFilter.Get1(0);

        ref var selCellUnitDataCom = ref _cellBuildFilter.Get1(selCom.IdxSelectedCell);
        ref var selOwnerCellUnitCom = ref _cellBuildFilter.Get2(selCom.IdxSelectedCell);

        if (selCom.IsSelectedCell && selCellUnitDataCom.IsBuildType(BuildingTypes.City))
        {
            if (selOwnerCellUnitCom.HaveOwner)
            {
                if (selOwnerCellUnitCom.IsMine)
                {
                    _buildZoneUIFilter.Get1(0).SetActiveZone(true);
                }
                else _buildZoneUIFilter.Get1(0).SetActiveZone(false);
            }
        }
        else
        {
            _buildZoneUIFilter.Get1(0).SetActiveZone(false);
        }
    }
}
