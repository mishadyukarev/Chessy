using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Leopotam.Ecs;

internal sealed class StatsUISystem : IEcsRunSystem
{
    private EcsFilter<SelectorCom> _selectorFilter = default;
    private EcsFilter<StatZoneViewUICom> _unitZoneUIFilter = default;

    private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
    private EcsFilter<CellBuildDataComponent> _cellBuildFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;

    public void Run()
    {
        var idxSelCell = _selectorFilter.Get1(0).IdxSelCell;

        ref var selUnitDatCom = ref _cellUnitFilter.Get1(idxSelCell);
        ref var selOwnUnitCom = ref _cellUnitFilter.Get2(idxSelCell);

        ref var selBuildDatCom = ref _cellBuildFilter.Get1(idxSelCell);
        ref var selEnvDatCom = ref _cellEnvFilter.Get1(idxSelCell);

        if (selUnitDatCom.HaveUnit)
        {
            var comPowerProtection = selUnitDatCom.PowerProtection
                + selBuildDatCom.PowerProtectionUnit(selUnitDatCom.UnitType, selUnitDatCom.SimplePowerDamage)
                + selEnvDatCom.PowerProtectionUnit(selUnitDatCom.UnitType);

            _unitZoneUIFilter.Get1(0).SetActiveStatZone(true);

            _unitZoneUIFilter.Get1(0).SetTextToStat(StatTypes.Health, selUnitDatCom.AmountHealth.ToString());
            _unitZoneUIFilter.Get1(0).SetTextToStat(StatTypes.Damage, selUnitDatCom.SimplePowerDamage.ToString());
            _unitZoneUIFilter.Get1(0).SetTextToStat(StatTypes.Protection, comPowerProtection.ToString());
            _unitZoneUIFilter.Get1(0).SetTextToStat(StatTypes.Steps, selUnitDatCom.AmountSteps.ToString());
        }

        else
        {
            _unitZoneUIFilter.Get1(0).SetActiveStatZone(false);
        }
    }
}
