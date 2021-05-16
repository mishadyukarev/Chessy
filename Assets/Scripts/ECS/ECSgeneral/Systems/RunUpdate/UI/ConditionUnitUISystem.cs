using Leopotam.Ecs;
using TMPro;
using static MainGame;

internal class ConditionUnitUISystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<SelectorComponent> _selectorComponentRef = default;

    private TextMeshProUGUI _hpCurrentUnitText;
    private TextMeshProUGUI _damageCurrentUnitText;
    private TextMeshProUGUI _protectionCurrentUnitText;
    private TextMeshProUGUI _stepsCurrentUnitText;

    private int[] _xySelectedCell => _selectorComponentRef.Unref().XYselectedCell;

    internal ConditionUnitUISystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _selectorComponentRef = eCSmanager.EntitiesGeneralManager.SelectorComponentRef;

        _hpCurrentUnitText = InstanceGame.GameObjectPool.HpCurrentUnitText;
        _damageCurrentUnitText = InstanceGame.GameObjectPool.DamageCurrentUnitText;
        _protectionCurrentUnitText = InstanceGame.GameObjectPool.ProtectionCurrentUnitText;
        _stepsCurrentUnitText = InstanceGame.GameObjectPool.StepsCurrentUnitText;
    }

    public void Run()
    {
        if (CellUnitComponent(_xySelectedCell).HaveUnit && CellUnitComponent(_xySelectedCell).IsMine)
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
            _hpCurrentUnitText.text = CellUnitComponent(_xySelectedCell).AmountHealth.ToString();
            _damageCurrentUnitText.text = CellUnitComponent(_xySelectedCell).SimplePowerDamage.ToString();
            _protectionCurrentUnitText.text = (CellUnitComponent(_xySelectedCell).PowerProtection(CellEnvironmentComponent(_xySelectedCell).ListEnvironmentTypes, CellBuildingComponent(_xySelectedCell).BuildingType)).ToString();
            _stepsCurrentUnitText.text = CellUnitComponent(_xySelectedCell).AmountSteps.ToString();
        }
    }
}
