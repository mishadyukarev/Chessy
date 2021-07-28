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
        if (CellUnitsDataWorker.HaveAnyUnit(XySelectedCell))
        {
            UIRightWorker.SetActiveParentZone(true, UnitUIZoneTypes.Stats);

            UIRightWorker.SetStatText(StatUITypes.Health, CellUnitsDataWorker.AmountHealth(XySelectedCell).ToString());
            UIRightWorker.SetStatText(StatUITypes.Damage, CellUnitsDataWorker.SimplePowerDamage(XySelectedCell).ToString());
            UIRightWorker.SetStatText(StatUITypes.Protiction, CellUnitsDataWorker.PowerProtection(XySelectedCell).ToString());
            UIRightWorker.SetStatText(StatUITypes.Steps, CellUnitsDataWorker.AmountSteps(XySelectedCell).ToString());
        }
        else
        {
            UIRightWorker.SetActiveParentZone(false, UnitUIZoneTypes.Stats);
        }
    }
}
