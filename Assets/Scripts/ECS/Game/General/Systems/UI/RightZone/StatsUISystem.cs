using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;

internal class StatsUISystem : SystemGeneralReduction
{
    private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.GetXy(SelectorCellTypes.Selected);
    private UnitTypes UnitType => _eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).UnitType;

    public override void Run()
    {
        base.Run();

        if (_eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).HaveAnyUnit)
        {

            _eGM.HealthUIEnt_TextMeshProUGUICom.SetText(_eGM.CellUnitEnt_CellUnitCom(XySelectedCell).AmountHealth.ToString());
            _eGM.PowerAttackUIEnt_TextMeshProUGUICom.SetText(CellUnitWorker.SimplePowerDamage(UnitType).ToString());
            _eGM.PowerProtectionUIEnt_TextMeshProUGUICom.SetText(CellUnitWorker.PowerProtection(XySelectedCell).ToString());
            _eGM.AmountStepsUIEnt_TextMeshProUGUICom.SetText(_eGM.CellUnitEnt_CellUnitCom(XySelectedCell).AmountSteps.ToString());

            _eGM.StatsEnt_ParentCom.SetActive(true);
        }
        else
        {
            _eGM.StatsEnt_ParentCom.SetActive(false);
        }
    }
}
