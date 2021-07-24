using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;

internal class StatsUISystem : SystemGeneralReduction
{
    private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);
    private UnitTypes UnitType => _eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).UnitType;

    public override void Run()
    {
        base.Run();

        if (CellUnitWorker.HaveAnyUnit(XySelectedCell))
        {

            _eGGUIM.HealthUIEnt_TextMeshProUGUICom.SetText(_eGM.CellUnitEnt_CellUnitCom(XySelectedCell).AmountHealth.ToString());
            _eGGUIM.PowerAttackUIEnt_TextMeshProUGUICom.SetText(CellUnitWorker.SimplePowerDamage(UnitType).ToString());
            _eGGUIM.PowerProtectionUIEnt_TextMeshProUGUICom.SetText(CellUnitWorker.PowerProtection(XySelectedCell).ToString());
            _eGGUIM.AmountStepsUIEnt_TextMeshProUGUICom.SetText(_eGM.CellUnitEnt_CellUnitCom(XySelectedCell).AmountSteps.ToString());

            _eGGUIM.StatsEnt_ParentCom.SetActive(true);
        }
        else
        {
            _eGGUIM.StatsEnt_ParentCom.SetActive(false);
        }
    }
}
