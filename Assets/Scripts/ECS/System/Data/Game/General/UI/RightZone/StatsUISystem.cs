using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;

internal class StatsUISystem : IEcsRunSystem
{
    private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);

    public void Run()
    {
        if (CellUnitsDataContainer.HaveAnyUnit(XySelectedCell))
        {
            RightUIViewContainer.SetActiveParentZone(true, UnitUIZoneTypes.Stats);

            RightUIViewContainer.SetStatText(StatUITypes.Health, CellUnitsDataContainer.AmountHealth(XySelectedCell).ToString());
            RightUIViewContainer.SetStatText(StatUITypes.Damage, CellUnitsDataContainer.SimplePowerDamage(XySelectedCell).ToString());
            RightUIViewContainer.SetStatText(StatUITypes.Protiction, CellUnitsDataContainer.PowerProtection(XySelectedCell).ToString());
            RightUIViewContainer.SetStatText(StatUITypes.Steps, CellUnitsDataContainer.AmountSteps(XySelectedCell).ToString());
        }
        else
        {
            RightUIViewContainer.SetActiveParentZone(false, UnitUIZoneTypes.Stats);
        }
    }
}
