using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;

internal sealed class StatsUISystem : IEcsRunSystem
{
    private EcsFilter<SelectorComponent> _selectorFilter = default;
    private EcsFilter<UnitZoneViewUICom> _unitZoneUIFilter = default;
    private int[] XySelectedCell => _selectorFilter.Get1(0).XySelectedCell;

    public void Run()
    {
        if (CellUnitsDataSystem.HaveAnyUnit(XySelectedCell))
        {
            _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Stats, true);

            _unitZoneUIFilter.Get1(0).SetTextToStat(StatTypes.Health, CellUnitsDataSystem.AmountHealth(XySelectedCell).ToString());
            _unitZoneUIFilter.Get1(0).SetTextToStat(StatTypes.Damage, CellUnitsDataSystem.SimplePowerDamage(XySelectedCell).ToString());
            _unitZoneUIFilter.Get1(0).SetTextToStat(StatTypes.Protection, CellUnitsDataSystem.PowerProtection(XySelectedCell).ToString());
            _unitZoneUIFilter.Get1(0).SetTextToStat(StatTypes.Steps, CellUnitsDataSystem.AmountSteps(XySelectedCell).ToString());
        }
        else
        {
            _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Stats, false);
        }
    }
}
