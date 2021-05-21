﻿using Leopotam.Ecs;
using TMPro;
using static MainGame;

internal class ConditionUnitUISystem : SystemGeneralReduction, IEcsRunSystem
{
    private EcsComponentRef<SelectorComponent> _selectorComponentRef = default;

    private TextMeshProUGUI _hpCurrentUnitText;
    private TextMeshProUGUI _damageCurrentUnitText;
    private TextMeshProUGUI _protectionCurrentUnitText;
    private TextMeshProUGUI _stepsCurrentUnitText;

    private int[] _xySelectedCell => _eGM.SelectorEntSelectorCom.XYselectedCell;

    internal ConditionUnitUISystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _hpCurrentUnitText = Instance.GameObjectPool.HpCurrentUnitText;
        _damageCurrentUnitText = Instance.GameObjectPool.DamageCurrentUnitText;
        _protectionCurrentUnitText = Instance.GameObjectPool.ProtectionCurrentUnitText;
        _stepsCurrentUnitText = Instance.GameObjectPool.StepsCurrentUnitText;
    }

    public void Run()
    {

        if (_eGM.CellUnitEnt_UnitTypeCom(_xySelectedCell).HaveUnit/* && _eGM.CellUnitComponent(_xySelectedCell).IsMine*/)
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
            _damageCurrentUnitText.text = _eGM.CellUnitEnt_CellUnitCom(_xySelectedCell).SimplePowerDamage.ToString();
            _protectionCurrentUnitText.text = (_eGM.CellUnitEnt_CellUnitCom(_xySelectedCell).PowerProtection.ToString());
            _stepsCurrentUnitText.text = _eGM.CellUnitEnt_CellUnitCom(_xySelectedCell).AmountSteps.ToString();
        }
    }
}
