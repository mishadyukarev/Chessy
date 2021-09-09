using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;

internal sealed class StatsUISystem : IEcsRunSystem
{
    private EcsFilter<SelectorComponent> _selectorFilter = default;
    private EcsFilter<UnitZoneViewUICom> _unitZoneUIFilter = default;

    private EcsFilter<CellUnitDataComponent, OwnerComponent, OwnerBotComponent> _cellUnitFilter = default;
    private EcsFilter<CellBuildDataComponent> _cellBuildFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;

    public void Run()
    {
        var idxSelCell = _selectorFilter.Get1(0).IdxSelectedCell;

        ref var selCellUnitDataCom = ref _cellUnitFilter.Get1(idxSelCell);
        ref var selOwnerCellUnitComp = ref _cellUnitFilter.Get2(idxSelCell);
        ref var selBotOwnerCellUnitComp = ref _cellUnitFilter.Get3(idxSelCell);

        ref var selCellBuildDataCom = ref _cellBuildFilter.Get1(idxSelCell);
        ref var selCellEnvDataCom = ref _cellEnvFilter.Get1(idxSelCell);

        if (selCellUnitDataCom.HaveUnit)
        {
            var comPowerProtection = selCellUnitDataCom.PowerProtection
                + selCellBuildDataCom.PowerProtectionUnit(selCellUnitDataCom.UnitType)
                + selCellEnvDataCom.PowerProtectionUnit(selCellUnitDataCom.UnitType);

            _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Stats, true);

            _unitZoneUIFilter.Get1(0).SetTextToStat(StatTypes.Health, selCellUnitDataCom.AmountHealth.ToString());
            _unitZoneUIFilter.Get1(0).SetTextToStat(StatTypes.Damage, selCellUnitDataCom.SimplePowerDamage.ToString());
            _unitZoneUIFilter.Get1(0).SetTextToStat(StatTypes.Protection, comPowerProtection.ToString());
            _unitZoneUIFilter.Get1(0).SetTextToStat(StatTypes.Steps, selCellUnitDataCom.AmountSteps.ToString()); 
        }

        else
        {
            _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Stats, false);
        }
    }
}
