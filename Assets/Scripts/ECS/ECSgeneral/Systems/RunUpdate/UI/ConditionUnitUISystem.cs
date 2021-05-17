using Leopotam.Ecs;
using TMPro;
using static MainGame;

internal class ConditionUnitUISystem : CellGeneralReduction, IEcsRunSystem
{
    private EcsComponentRef<SelectorComponent> _selectorComponentRef = default;

    private TextMeshProUGUI _hpCurrentUnitText;
    private TextMeshProUGUI _damageCurrentUnitText;
    private TextMeshProUGUI _protectionCurrentUnitText;
    private TextMeshProUGUI _stepsCurrentUnitText;

    private int[] _xySelectedCell => _eGM.SelectorComponentSelectorEnt.XYselectedCell;

    internal ConditionUnitUISystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _hpCurrentUnitText = InstanceGame.GameObjectPool.HpCurrentUnitText;
        _damageCurrentUnitText = InstanceGame.GameObjectPool.DamageCurrentUnitText;
        _protectionCurrentUnitText = InstanceGame.GameObjectPool.ProtectionCurrentUnitText;
        _stepsCurrentUnitText = InstanceGame.GameObjectPool.StepsCurrentUnitText;
    }

    public void Run()
    {
        if (_eGM.CellUnitComponent(_xySelectedCell).HaveUnit && _eGM.CellUnitComponent(_xySelectedCell).IsMine)
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
            _hpCurrentUnitText.text = _eGM.CellUnitComponent(_xySelectedCell).AmountHealth.ToString();
            _damageCurrentUnitText.text = _eGM.CellUnitComponent(_xySelectedCell).SimplePowerDamage.ToString();
            _protectionCurrentUnitText.text = (_eGM.CellUnitComponent(_xySelectedCell).PowerProtection(_eGM.CellEnvironmentComponent(_xySelectedCell).ListEnvironmentTypes, _eGM.CellBuildingComponent(_xySelectedCell).BuildingType)).ToString();
            _stepsCurrentUnitText.text = _eGM.CellUnitComponent(_xySelectedCell).AmountSteps.ToString();
        }
    }
}
