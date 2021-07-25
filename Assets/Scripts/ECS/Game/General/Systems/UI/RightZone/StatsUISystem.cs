using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;

internal class StatsUISystem : SystemGeneralReduction
{
    private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);
    private UnitTypes UnitType => CellUnitsDataWorker.UnitType(XySelectedCell);

    public override void Run()
    {
        base.Run();

        if (CellUnitsDataWorker.HaveAnyUnit(XySelectedCell))
        {

            _eGGUIM.HealthUIEnt_TextMeshProUGUICom.SetText(CellUnitsDataWorker.AmountHealth(XySelectedCell).ToString());
            _eGGUIM.PowerAttackUIEnt_TextMeshProUGUICom.SetText(CellUnitsDataWorker.SimplePowerDamage(UnitType).ToString());
            _eGGUIM.PowerProtectionUIEnt_TextMeshProUGUICom.SetText(CellUnitsDataWorker.PowerProtection(XySelectedCell).ToString());
            _eGGUIM.AmountStepsUIEnt_TextMeshProUGUICom.SetText(CellUnitsDataWorker.AmountSteps(XySelectedCell).ToString());

            _eGGUIM.StatsEnt_ParentCom.SetActive(true);
        }
        else
        {
            _eGGUIM.StatsEnt_ParentCom.SetActive(false);
        }
    }
}
