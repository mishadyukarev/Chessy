using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;

internal class StatsUISystem : IEcsRunSystem
{
    private int[] XySelectedCell => SelectorSystem.XySelectedCell;

    public void Run()
    {
        if (CellUnitsDataSystem.HaveAnyUnit(XySelectedCell))
        {
            RightUIViewContainer.SetActiveParentZone(true, UnitUIZoneTypes.Stats);

            RightUIViewContainer.SetStatText(StatUITypes.Health, CellUnitsDataSystem.AmountHealth(XySelectedCell).ToString());
            RightUIViewContainer.SetStatText(StatUITypes.Damage, CellUnitsDataSystem.SimplePowerDamage(XySelectedCell).ToString());
            RightUIViewContainer.SetStatText(StatUITypes.Protiction, CellUnitsDataSystem.PowerProtection(XySelectedCell).ToString());
            RightUIViewContainer.SetStatText(StatUITypes.Steps, CellUnitsDataSystem.AmountSteps(XySelectedCell).ToString());
        }
        else
        {
            RightUIViewContainer.SetActiveParentZone(false, UnitUIZoneTypes.Stats);
        }
    }
}
