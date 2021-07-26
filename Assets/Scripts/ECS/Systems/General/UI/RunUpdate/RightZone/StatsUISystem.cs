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

            _eGGUIM.HealthUIEnt_TextMeshProUGUICom.TextMeshProUGUI.text = CellUnitsDataWorker.AmountHealth(XySelectedCell).ToString();
            _eGGUIM.PowerAttackUIEnt_TextMeshProUGUICom.TextMeshProUGUI.text = CellUnitsDataWorker.SimplePowerDamage(UnitType).ToString();
            _eGGUIM.PowerProtectionUIEnt_TextMeshProUGUICom.TextMeshProUGUI.text = CellUnitsDataWorker.PowerProtection(XySelectedCell).ToString();
            _eGGUIM.AmountStepsUIEnt_TextMeshProUGUICom.TextMeshProUGUI.text = CellUnitsDataWorker.AmountSteps(XySelectedCell).ToString();

            _eGGUIM.StatsEnt_ParentCom.ParentGO.SetActive(true);
        }
        else
        {
            _eGGUIM.StatsEnt_ParentCom.ParentGO.SetActive(false);
        }
    }
}
