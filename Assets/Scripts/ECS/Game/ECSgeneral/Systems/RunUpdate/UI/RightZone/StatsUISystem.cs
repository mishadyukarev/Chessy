using TMPro;

internal class StatsUISystem : SystemGeneralReduction
{
    private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.XySelectedCell;

    public override void Run()
    {
        base.Run();

        if (_eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).HaveUnit)
        {
            _eGM.StatsEnt_ParentCom.SetActive(true);
        }
        else
        {
            _eGM.StatsEnt_ParentCom.SetActive(false);
        }

        _eGM.HealthUIEnt_TextMeshProUGUICom.Text = _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).AmountHealth.ToString();
        _eGM.PowerAttackUIEnt_TextMeshProUGUICom.Text = _cM.CellUnitWorker.SimplePowerDamage(XySelectedCell).ToString();
        _eGM.PowerProtectionUIEnt_TextMeshProUGUICom.Text = _cM.CellUnitWorker.PowerProtection(XySelectedCell).ToString();
        _eGM.AmountStepsUIEnt_TextMeshProUGUICom.Text = _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).AmountSteps.ToString();
    }
}
