using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Leopotam.Ecs;

internal sealed class StatsUISystem : IEcsRunSystem
{
    private EcsFilter<SelectorComponent> _selectorFilter = default;
    private EcsFilter<UnitZoneViewUICom> _unitZoneUIFilter = default;

    private EcsFilter<CellUnitDataComponent> _cellUnitFilter = default;

    public void Run()
    {
        var idxSelCell = _selectorFilter.Get1(0).IdxSelectedCell;

        ref var selCellUnitDataCom = ref _cellUnitFilter.Get1(idxSelCell);

        if (selCellUnitDataCom.HaveUnit)
        {
            _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Stats, true);

            _unitZoneUIFilter.Get1(0).SetTextToStat(StatTypes.Health, selCellUnitDataCom.AmountHealth.ToString());
            _unitZoneUIFilter.Get1(0).SetTextToStat(StatTypes.Damage, selCellUnitDataCom.SimplePowerDamage.ToString());
            //_unitZoneUIFilter.Get1(0).SetTextToStat(StatTypes.Protection, selCellUnitDataCom.PowerProtection.ToString());
            _unitZoneUIFilter.Get1(0).SetTextToStat(StatTypes.Steps, selCellUnitDataCom.AmountSteps.ToString());
        }
        else
        {
            _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Stats, false);
        }
    }
}
