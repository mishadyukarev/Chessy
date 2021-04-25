﻿using Leopotam.Ecs;
using TMPro;

internal class ConditionUnitUISystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<SelectorComponent> _selectorComponentRef = default;
    private EcsComponentRef<SelectorUnitComponent> _selectorUnitComponentRef = default;
    private EcsComponentRef<EconomyComponent.UnitsComponent> _economyUnitsComponentRef = default;
    private EcsComponentRef<DonerComponent> _doneComponentRef = default;

    private PhotonPunRPC _photonPunRPC = default;

    private TextMeshProUGUI _hpCurrentUnitText;
    private TextMeshProUGUI _damageCurrentUnitText;
    private TextMeshProUGUI _protectionCurrentUnitText;
    private TextMeshProUGUI _stepsCurrentUnitText;

    private int[] _xySelectedCell => _selectorComponentRef.Unref().XYselectedCell;

    internal ConditionUnitUISystem(ECSmanager eCSmanager, SupportGameManager supportGameManager, PhotonGameManager photonManager, StartSpawnGameManager startSpawnGameManager) : base(eCSmanager, supportGameManager)
    {
        _photonPunRPC = photonManager.PhotonPunRPC;

        _selectorComponentRef = eCSmanager.EntitiesGeneralManager.SelectorComponentRef;
        _selectorUnitComponentRef = eCSmanager.EntitiesGeneralManager.SelectorUnitComponent;
        _economyUnitsComponentRef = eCSmanager.EntitiesGeneralManager.EconomyUnitsComponentRef;
        _doneComponentRef = eCSmanager.EntitiesGeneralManager.DonerComponentRef;

        _hpCurrentUnitText = startSpawnGameManager.HpCurrentUnitText;
        _damageCurrentUnitText = startSpawnGameManager.DamageCurrentUnitText;
        _protectionCurrentUnitText = startSpawnGameManager.ProtectionCurrentUnitText;
        _stepsCurrentUnitText = startSpawnGameManager.StepsCurrentUnitText;
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
            _damageCurrentUnitText.text = CellUnitComponent(_xySelectedCell).PowerDamage.ToString();
            _protectionCurrentUnitText.text
                = (CellUnitComponent(_xySelectedCell).PowerProtection
                + CellEnvironmentComponent(_xySelectedCell).PowerProtection
                + CellBuildingComponent(_xySelectedCell).PowerProtection)
                .ToString();
            _stepsCurrentUnitText.text = CellUnitComponent(_xySelectedCell).AmountSteps.ToString();
        }
    }
}
