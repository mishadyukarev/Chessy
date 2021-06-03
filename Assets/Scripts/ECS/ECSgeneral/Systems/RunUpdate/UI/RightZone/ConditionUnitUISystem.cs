using TMPro;
using static Main;

internal class ConditionUnitUISystem : SystemGeneralReduction
{
    private TextMeshProUGUI _hpCurrentUnitText;
    private TextMeshProUGUI _damageCurrentUnitText;
    private TextMeshProUGUI _protectionCurrentUnitText;
    private TextMeshProUGUI _stepsCurrentUnitText;

    private int[] _xySelectedCell => _eGM.SelectorEntSelectorCom.XYselectedCell;

    internal ConditionUnitUISystem()
    {
        _hpCurrentUnitText = Instance.CanvasGameManager.HpCurrentUnitText;
        _damageCurrentUnitText = Instance.CanvasGameManager.DamageCurrentUnitText;
        _protectionCurrentUnitText = Instance.CanvasGameManager.ProtectionCurrentUnitText;
        _stepsCurrentUnitText = Instance.CanvasGameManager.StepsCurrentUnitText;
    }

    public override void Run()
    {
        base.Run();

        if (_eGM.CellUnitEnt_UnitTypeCom(_xySelectedCell).HaveUnit)
        {
            ActiveteSupportTextForAbilities(true);
        }
        else
        {
            ActiveteSupportTextForAbilities(false);
        }
    }

    private void ActiveteSupportTextForAbilities(bool isActive)
    {
        _hpCurrentUnitText.gameObject.SetActive(isActive);
        _damageCurrentUnitText.gameObject.SetActive(isActive);
        _protectionCurrentUnitText.gameObject.SetActive(isActive);
        _stepsCurrentUnitText.gameObject.SetActive(isActive);

        if (isActive)
        {
            _hpCurrentUnitText.text = _eGM.CellUnitEnt_CellUnitCom(_xySelectedCell).AmountHealth.ToString();
            _damageCurrentUnitText.text = _cM.CellUnitWorker.SimplePowerDamage(_xySelectedCell).ToString();
            _protectionCurrentUnitText.text = _cM.CellUnitWorker.PowerProtection(_xySelectedCell).ToString();
            _stepsCurrentUnitText.text = _eGM.CellUnitEnt_CellUnitCom(_xySelectedCell).AmountSteps.ToString();
        }
    }
}
