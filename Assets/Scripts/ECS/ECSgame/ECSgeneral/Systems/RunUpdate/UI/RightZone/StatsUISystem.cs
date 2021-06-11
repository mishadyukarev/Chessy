using TMPro;
using static Main;

internal class StatsUISystem : SystemGeneralReduction
{
    private TextMeshProUGUI _hpCurrentUnitText;
    private TextMeshProUGUI _damageCurrentUnitText;
    private TextMeshProUGUI _protectionCurrentUnitText;
    private TextMeshProUGUI _stepsCurrentUnitText;

    private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.XySelectedCell;

    internal StatsUISystem()
    {
        //_hpCurrentUnitText = Instance.CanvasGameManager.HpCurrentUnitText;
        //_damageCurrentUnitText = Instance.CanvasGameManager.DamageCurrentUnitText;
        //_protectionCurrentUnitText = Instance.CanvasGameManager.ProtectionCurrentUnitText;
        //_stepsCurrentUnitText = Instance.CanvasGameManager.StepsCurrentUnitText;
    }

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

        //    _hpCurrentUnitText.text = _eGM.CellUnitEnt_CellUnitCom(_xySelectedCell).AmountHealth.ToString();
        //    _damageCurrentUnitText.text = _cM.CellUnitWorker.SimplePowerDamage(_xySelectedCell).ToString();
        //    _protectionCurrentUnitText.text = _cM.CellUnitWorker.PowerProtection(_xySelectedCell).ToString();
        //    _stepsCurrentUnitText.text = _eGM.CellUnitEnt_CellUnitCom(_xySelectedCell).AmountSteps.ToString();
    }
}
