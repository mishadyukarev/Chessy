using Assets.Scripts;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.Components;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Info;
using ExitGames.Client.Photon.StructWrapping;
using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;
using static Assets.Scripts.Abstractions.ValuesConsts.EnvironmentValues;
using static Assets.Scripts.CellEnvironmentWorker;
using static Assets.Scripts.Main;

public sealed class EntitiesGameGeneralManager : EntitiesManager
{
    internal GameObject BackGroundGO;
    internal SpriteRenderer BackGroundSR;
    internal AudioSource MistakeAudioSource;
    internal AudioSource AttackAudioSource;


    #region Cells

    private EcsEntity[,] _cellProtectRelaxEnts;
    internal ref SpriteRendererComponent CellProtectRelaxEnt_SpriteRendererCom(int[] xy) => ref _cellProtectRelaxEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


    private EcsEntity[,] _cellMaxStepsEnts;
    internal ref SpriteRendererComponent CellMaxStepsEnt_SpriteRendererCom(int[] xy) => ref _cellMaxStepsEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


    private EcsEntity[,] _cellUnitEnts;
    internal ref CellUnitComponent CellUnitEnt_CellUnitCom(int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>();
    internal ref OwnerComponent CellUnitEnt_CellOwnerCom(int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<OwnerComponent>();
    internal ref OwnerBotComponent CellUnitEnt_CellOwnerBotCom(int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<OwnerBotComponent>();
    internal ref UnitTypeComponent CellUnitEnt_UnitTypeCom(int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<UnitTypeComponent>();
    internal ref ActivatedDictComponent CellUnitEnt_ActivatedForPlayersCom(int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<ActivatedDictComponent>();
    internal ref ProtectRelaxComponent CellUnitEnt_ProtectRelaxCom(int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<ProtectRelaxComponent>();
    internal ref SpriteRendererComponent CellUnitEnt_SpriteRendererCom(int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


    #region SupportStatic

    private EcsEntity[,] _cellHpSupStatEnts;
    internal ref SpriteRendererComponent CellHpSupStatEnt_SpriteRendererCom(int[] xy) => ref _cellHpSupStatEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


    private EcsEntity[,] _cellFertilizeSupStatEnts;
    internal ref SpriteRendererComponent CellFertilizeSupStatEnt_SprRendCom(int[] xy) => ref _cellFertilizeSupStatEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


    private EcsEntity[,] _cellForestSupStatEnts;
    internal ref SpriteRendererComponent CellWoodSupStatEnt_SprRendCom(int[] xy) => ref _cellForestSupStatEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


    private EcsEntity[,] _cellOreSupStatEnts;
    internal ref SpriteRendererComponent CellOreSupStatEnt_SprRendCom(int[] xy) => ref _cellOreSupStatEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();

    #endregion


    private EcsEntity[,] _cellSupportVisionEnts;
    internal ref SpriteRendererComponent CellSupVisEnt_SpriteRenderer(int[] xy) => ref _cellSupportVisionEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


    private EcsEntity[,] _cellFireEnts;
    internal ref SpriteRendererComponent CellFireEnt_SprRendCom(int[] xy) => ref _cellFireEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();
    internal ref HaverEffectComponent CellFireEnt_HaverEffectCom(int[] xy) => ref _cellFireEnts[xy[X], xy[Y]].Get<HaverEffectComponent>();
    internal ref TimeStepsComponent CellFireEnt_TimeStepsCom(int[] xy) => ref _cellFireEnts[xy[X], xy[Y]].Get<TimeStepsComponent>();


    private EcsEntity[,] _cellBuildingEnts;
    internal ref CellBuildingComponent CellBuildEnt_CellBuilCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<CellBuildingComponent>();
    internal ref BuildingTypeComponent CellBuildEnt_BuilTypeCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<BuildingTypeComponent>();
    internal ref CellComponent CellBuildEnt_CellCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<CellComponent>();
    internal ref OwnerComponent CellBuildEnt_OwnerCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<OwnerComponent>();
    internal ref OwnerBotComponent CellBuildEnt_OwnerBotCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<OwnerBotComponent>();
    internal ref SpriteRendererComponent CellBuildEnt_SpriteRendererCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


    #region CellEnvEnts

    private EcsEntity[,] _cellFertilizerEnts;
    internal ref SpriteRendererComponent CellFertilizerEnt_SprRendCom(int[] xy) => ref _cellFertilizerEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();
    internal ref AmountResourcesComponent CellFertilizerEnt_AmountResourcesCom(int[] xy) => ref _cellFertilizerEnts[xy[X], xy[Y]].Get<AmountResourcesComponent>();
    internal ref HaveEnvironmentComponent CellFertilizerEnt_HaveEnvCom(int[] xy) => ref _cellFertilizerEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();


    private EcsEntity[,] _cellYoungForestEnts;
    internal ref SpriteRendererComponent CellYoungForestEnt_SprRendCom(int[] xy) => ref _cellYoungForestEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();
    internal ref HaveEnvironmentComponent CellYoungForestEnt_HaveEnvCom(int[] xy) => ref _cellYoungForestEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();


    private EcsEntity[,] _cellAdultForestEnts;
    internal ref SpriteRendererComponent CellAdultForestEnt_SprRendCom(int[] xy) => ref _cellAdultForestEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();
    internal ref AmountResourcesComponent CellAdultForestEnt_AmountResourcesCom(int[] xy) => ref _cellAdultForestEnts[xy[X], xy[Y]].Get<AmountResourcesComponent>();
    internal ref HaveEnvironmentComponent CellAdultForestEnt_HaveEnvCom(int[] xy) => ref _cellAdultForestEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();


    private EcsEntity[,] _cellHillEnts;
    internal ref SpriteRendererComponent CellHillEnt_SprRendCom(int[] xy) => ref _cellHillEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();
    internal ref AmountResourcesComponent CellHillEnt_AmountResourcesCom(int[] xy) => ref _cellHillEnts[xy[X], xy[Y]].Get<AmountResourcesComponent>();
    internal ref HaveEnvironmentComponent CellHillEnt_HaveEnvCom(int[] xy) => ref _cellHillEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();


    private EcsEntity[,] _cellMountainEnts;
    internal ref SpriteRendererComponent CellMountainEnt_SprRendCom(int[] xy) => ref _cellMountainEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();
    internal ref HaveEnvironmentComponent CellMountainEnt_HaveEnvCom(int[] xy) => ref _cellMountainEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();

    #endregion


    private EcsEntity[,] _cellEnts;
    internal ref CellBaseComponent CellEnt_CellBaseCom(int[] xy) => ref _cellEnts[xy[X], xy[Y]].Get<CellBaseComponent>();
    internal ref XyCellComponent CellEnt_XyCellCom(int[] xy) => ref _cellEnts[xy[X], xy[Y]].Get<XyCellComponent>();


    internal int Xamount => _cellEnts.GetUpperBound(X) + 1;
    internal int Yamount => _cellEnts.GetUpperBound(Y) + 1;

    #endregion


    #region Selector

    private EcsEntity _selectorEnt;
    internal ref SelectorComponent SelectorEnt_SelectorCom => ref _selectorEnt.Get<SelectorComponent>();
    internal ref RaycastHit2DComponent SelectorEnt_RayCom => ref _selectorEnt.Get<RaycastHit2DComponent>();
    internal ref UnitTypeComponent SelectorEnt_UnitTypeCom => ref _selectorEnt.Get<UnitTypeComponent>();
    internal ref UpgradeModTypeComponent SelectorEnt_UpgradeModTypeCom => ref _selectorEnt.Get<UpgradeModTypeComponent>();


    private EcsEntity _xyCurrentCellEnt;
    internal ref XyCellComponent XyCurrentCellEnt_XyCellCom => ref _xyCurrentCellEnt.Get<XyCellComponent>();


    private EcsEntity _xySelectedCellEnt;
    internal ref XyCellComponent XySelectedCellEnt_XyCellCom => ref _xySelectedCellEnt.Get<XyCellComponent>();


    private EcsEntity _xyPreviousCellEnt;
    internal ref XyCellComponent XyPreviousCellEnt_XyCellCom => ref _xyPreviousCellEnt.Get<XyCellComponent>();


    private EcsEntity _xyPreviousVisionCellEnt;
    internal ref XyCellComponent XyPreviousVisionCellEnt_XyCellCom => ref _xyPreviousVisionCellEnt.Get<XyCellComponent>();


    private EcsEntity _availableCellsSettingEnt;
    internal ref AvailableCellsComponent AvailableCellsSettingEnt_AvailCellsCom => ref _availableCellsSettingEnt.Get<AvailableCellsComponent>();


    private EcsEntity _availableCellsShiftEnt;
    internal ref AvailableCellsComponent AvailableCellsShiftEnt_AvailCellsCom => ref _availableCellsShiftEnt.Get<AvailableCellsComponent>();


    private EcsEntity _availableCellsSimpleAttackEnt;
    internal ref AvailableCellsComponent AvailableCellsSimpleAttackEnt_AvailCellsCom => ref _availableCellsSimpleAttackEnt.Get<AvailableCellsComponent>();


    private EcsEntity _availableCellsUniqueAttackEnt;
    internal ref AvailableCellsComponent AvailableCellsUniqueAttackEnt_AvailCellsCom => ref _availableCellsUniqueAttackEnt.Get<AvailableCellsComponent>();

    #endregion


    #region Info

    #region Units

    private EcsEntity _kingInfoEnt;
    internal ref AmountUnitsInGameComponent KingInfoEnt_AmountUnitsInGameCom => ref _kingInfoEnt.Get<AmountUnitsInGameComponent>();
    internal ref AmountUnitsInInventorDictComponent KingInfoEnt_AmountUnitsInInventorDictCom => ref _kingInfoEnt.Get<AmountUnitsInInventorDictComponent>();
    internal ref IsSettedUnitDictComponent KingInfoEnt_IsSettedUnitDictCom => ref _kingInfoEnt.Get<IsSettedUnitDictComponent>();
    internal ref XySettedBuildingDictComponent KingInfoEnt_XySettedBuildingDictCom => ref _kingInfoEnt.Get<XySettedBuildingDictComponent>();
    internal ref AmountUnitsInStandartConditionComponent KingInfoEnt_AmountUnitsInRelaxCom => ref _kingInfoEnt.Get<AmountUnitsInStandartConditionComponent>();


    private EcsEntity _pawnInfoEnt;
    internal ref AmountUnitsInGameComponent PawnInfoEnt_AmountUnitsInGameCom => ref _pawnInfoEnt.Get<AmountUnitsInGameComponent>();
    internal ref AmountUnitsInInventorDictComponent PawnInfoEnt_AmountUnitsInInventorDictCom => ref _pawnInfoEnt.Get<AmountUnitsInInventorDictComponent>();
    internal ref AmountUnitsInStandartConditionComponent PawnInfoEnt_AmountUnitsInRelaxCom => ref _pawnInfoEnt.Get<AmountUnitsInStandartConditionComponent>();



    private EcsEntity _pawnSwordInfoEnt;
    internal ref AmountUnitsInGameComponent PawnSwordInfoEnt_AmountUnitsInGameCom => ref _pawnSwordInfoEnt.Get<AmountUnitsInGameComponent>();
    internal ref AmountUnitsInInventorDictComponent PawnSwordInfoEnt_AmountUnitsInInventorDictCom => ref _pawnSwordInfoEnt.Get<AmountUnitsInInventorDictComponent>();
    internal ref AmountUnitsInStandartConditionComponent PawnSwordInfoEnt_AmountUnitsInRelaxCom => ref _pawnSwordInfoEnt.Get<AmountUnitsInStandartConditionComponent>();


    private EcsEntity _rookInfoEnt;
    internal ref AmountUnitsInGameComponent RookInfoEnt_AmountUnitsInGameCom => ref _rookInfoEnt.Get<AmountUnitsInGameComponent>();
    internal ref AmountUnitsInInventorDictComponent RookInfoEnt_AmountUnitsInInventorDictCom => ref _rookInfoEnt.Get<AmountUnitsInInventorDictComponent>();
    internal ref AmountUnitsInStandartConditionComponent RookInfoEnt_AmountUnitsInRelaxCom => ref _rookInfoEnt.Get<AmountUnitsInStandartConditionComponent>();


    private EcsEntity _rookCrossbowInfoEnt;
    internal ref AmountUnitsInGameComponent RookCrossbowInfoEnt_AmountUnitsInGameCom => ref _rookCrossbowInfoEnt.Get<AmountUnitsInGameComponent>();
    internal ref AmountUnitsInInventorDictComponent RookCrossbowInfoEnt_AmountUnitsInInventorDictCom => ref _rookCrossbowInfoEnt.Get<AmountUnitsInInventorDictComponent>();
    internal ref AmountUnitsInStandartConditionComponent RookCrossbowInfoEnt_AmountUnitsInRelaxCom => ref _rookCrossbowInfoEnt.Get<AmountUnitsInStandartConditionComponent>();


    private EcsEntity _bishopInfoInGameEnt;
    internal ref AmountUnitsInGameComponent BishopInfoEnt_AmountUnitsInGameCom => ref _bishopInfoInGameEnt.Get<AmountUnitsInGameComponent>();
    internal ref AmountUnitsInInventorDictComponent BishopInfoEnt_AmountUnitsInInventorDictCom => ref _bishopInfoInGameEnt.Get<AmountUnitsInInventorDictComponent>();
    internal ref AmountUnitsInStandartConditionComponent BishopInfoEnt_AmountUnitsInRelaxCom => ref _bishopInfoInGameEnt.Get<AmountUnitsInStandartConditionComponent>();


    private EcsEntity _bishopCrossbowInfoEnt;
    internal ref AmountUnitsInGameComponent BishopCrossbowInfoEnt_AmountUnitsInGameCom => ref _bishopCrossbowInfoEnt.Get<AmountUnitsInGameComponent>();
    internal ref AmountUnitsInInventorDictComponent BishopCrossbowInfoEnt_AmountUnitsInInventorDictCom => ref _bishopCrossbowInfoEnt.Get<AmountUnitsInInventorDictComponent>();
    internal ref AmountUnitsInStandartConditionComponent BishopCrossbowInfoEnt_AmountUnitsInStandartCondition => ref _bishopCrossbowInfoEnt.Get<AmountUnitsInStandartConditionComponent>();

    #endregion


    #region Buildings

    private EcsEntity _cityInfoEnt;
    internal ref BuildingsInGameDictComponent CityInfoEnt_AmountBuildingsInGameCom => ref _cityInfoEnt.Get<BuildingsInGameDictComponent>();


    private EcsEntity _farmsInfoEnt;
    internal ref BuildingsInGameDictComponent FarmsInfoEnt_AmountBuildingsInGameCom => ref _farmsInfoEnt.Get<BuildingsInGameDictComponent>();
    internal ref AmountUpgradesDictComponent FarmsInfoEnt_AmountUpgradesCom => ref _farmsInfoEnt.Get<AmountUpgradesDictComponent>();


    private EcsEntity _woodcuttersInfoEnt;
    internal ref BuildingsInGameDictComponent WoodcuttersInfoEnt_AmountBuildingsInGameCom => ref _woodcuttersInfoEnt.Get<BuildingsInGameDictComponent>();
    internal ref AmountUpgradesDictComponent WoodcuttersInfoEnt_AmountUpgradesCom => ref _woodcuttersInfoEnt.Get<AmountUpgradesDictComponent>();


    private EcsEntity _minesInfoEnt;
    internal ref BuildingsInGameDictComponent MinesInfoEnt_AmountBuildingsInGameCom => ref _minesInfoEnt.Get<BuildingsInGameDictComponent>();
    internal ref AmountUpgradesDictComponent MinesInfoEnt_AmountUpgradesCom => ref _minesInfoEnt.Get<AmountUpgradesDictComponent>();

    #endregion


    #region Economy

    private EcsEntity _foodInfoEnt;
    internal ref AmountResourcesDictComponent FoodInfoEnt_AmountResourcesDictCom => ref _foodInfoEnt.Get<AmountResourcesDictComponent>();


    private EcsEntity _woodInfoEnt;
    internal ref AmountResourcesDictComponent WoodInfoEnt_AmountResourcesDictCom => ref _woodInfoEnt.Get<AmountResourcesDictComponent>();


    private EcsEntity _oreInfoEnt;
    internal ref AmountResourcesDictComponent OreInfoEnt_AmountResourcesDictCom => ref _oreInfoEnt.Get<AmountResourcesDictComponent>();


    private EcsEntity _ironInfoEnt;
    internal ref AmountResourcesDictComponent IronInfoEnt_AmountResourcesDictCom => ref _ironInfoEnt.Get<AmountResourcesDictComponent>();


    private EcsEntity _goldInfoEnt;
    internal ref AmountResourcesDictComponent GoldInfoEnt_AmountResourcesDictCom => ref _goldInfoEnt.Get<AmountResourcesDictComponent>();

    #endregion

    #endregion


    #region Music

    private EcsEntity _attackArcherSoundEnt;
    internal ref AudioSourceComponent AttackArcherEnt_AudioSourceCom => ref _attackArcherSoundEnt.Get<AudioSourceComponent>();


    private EcsEntity _pickArcherSoundEnt;
    internal ref AudioSourceComponent PickArcherEnt_AudioSourceCom => ref _pickArcherSoundEnt.Get<AudioSourceComponent>();


    private EcsEntity _pickMeleeSoundEnt;
    internal ref AudioSourceComponent PickMeleeEnt_AudioSourceCom => ref _pickMeleeSoundEnt.Get<AudioSourceComponent>();


    private EcsEntity _buildingSoundEnt;
    internal ref AudioSourceComponent BuildingSoundEnt_AudioSourceCom => ref _buildingSoundEnt.Get<AudioSourceComponent>();


    private EcsEntity _fireSoundEnt;
    internal ref AudioSourceComponent FireSoundEnt_AudioSourceCom => ref _fireSoundEnt.Get<AudioSourceComponent>();


    private EcsEntity _settingSoundEnt;
    internal ref AudioSourceComponent SettingSoundEnt_AudioSourceCom => ref _settingSoundEnt.Get<AudioSourceComponent>();


    private EcsEntity _buySoundEnt;
    internal ref AudioSourceComponent BuySoundEnt_AudioSourceCom => ref _buySoundEnt.Get<AudioSourceComponent>();


    private EcsEntity _meltingSoundEnt;
    internal ref AudioSourceComponent MeltingSoundEnt_AudioSourceCom => ref _meltingSoundEnt.Get<AudioSourceComponent>();


    private EcsEntity _destroySoundEnt;
    internal ref AudioSourceComponent DestroySoundEnt_AudioSourceCom => ref _destroySoundEnt.Get<AudioSourceComponent>();


    private EcsEntity _upgradeUnitMeleeSoundEnt;
    internal ref AudioSourceComponent UpgradeUnitMeleeSoundEnt_AudioSourceCom => ref _upgradeUnitMeleeSoundEnt.Get<AudioSourceComponent>();


    private EcsEntity _seedingSoundEnt;
    internal ref AudioSourceComponent SeedingSoundEnt_AudioSourceCom => ref _seedingSoundEnt.Get<AudioSourceComponent>();


    private EcsEntity _shiftUnitSoundEnt;
    internal ref AudioSourceComponent ShiftUnitSoundEnt_AudioSourceCom => ref _shiftUnitSoundEnt.Get<AudioSourceComponent>();


    private EcsEntity _truceSoundEnt;
    internal ref AudioSourceComponent TruceSoundEnt_AudioSourceCom => ref _truceSoundEnt.Get<AudioSourceComponent>();


    #endregion


    #region Else

    private EcsEntity _inputEnt;
    internal ref InputComponent InputEnt_InputCom => ref _inputEnt.Get<InputComponent>();


    private EcsEntity _fromInfoEnt;
    internal ref FromInfoComponent FromInfoEnt_FromInfoCom => ref _fromInfoEnt.Get<FromInfoComponent>();

    #endregion


    internal override void FillEntities(EcsWorld gameWorld)
    {
        base.FillEntities(gameWorld);


        #region Selector

        _selectorEnt = gameWorld.NewEntity();

        _xyCurrentCellEnt = gameWorld.NewEntity();
        _xySelectedCellEnt = gameWorld.NewEntity();
        _xyPreviousCellEnt = gameWorld.NewEntity();
        _xyPreviousVisionCellEnt = gameWorld.NewEntity();

        _availableCellsSettingEnt = gameWorld.NewEntity();
        _availableCellsShiftEnt = gameWorld.NewEntity();
        _availableCellsSimpleAttackEnt = gameWorld.NewEntity();
        _availableCellsUniqueAttackEnt = gameWorld.NewEntity();

        #endregion


        #region Info

        _kingInfoEnt = gameWorld.NewEntity();
        _pawnInfoEnt = gameWorld.NewEntity();
        _pawnSwordInfoEnt = gameWorld.NewEntity();
        _rookInfoEnt = gameWorld.NewEntity();
        _rookCrossbowInfoEnt = gameWorld.NewEntity();
        _bishopInfoInGameEnt = gameWorld.NewEntity();
        _bishopCrossbowInfoEnt = gameWorld.NewEntity();

        _cityInfoEnt = gameWorld.NewEntity();
        _farmsInfoEnt = gameWorld.NewEntity();
        _woodcuttersInfoEnt = gameWorld.NewEntity();
        _minesInfoEnt = gameWorld.NewEntity();

        _foodInfoEnt = gameWorld.NewEntity();
        _woodInfoEnt = gameWorld.NewEntity();
        _oreInfoEnt = gameWorld.NewEntity();
        _ironInfoEnt = gameWorld.NewEntity();
        _goldInfoEnt = gameWorld.NewEntity();

        #endregion

        _attackArcherSoundEnt = gameWorld.NewEntity();
        _pickArcherSoundEnt = gameWorld.NewEntity();
        _pickMeleeSoundEnt = gameWorld.NewEntity();
        _buildingSoundEnt = gameWorld.NewEntity();
        _fireSoundEnt = gameWorld.NewEntity();
        _settingSoundEnt = gameWorld.NewEntity();
        _buySoundEnt = gameWorld.NewEntity();
        _meltingSoundEnt = gameWorld.NewEntity();
        _destroySoundEnt = gameWorld.NewEntity();
        _upgradeUnitMeleeSoundEnt = gameWorld.NewEntity();
        _seedingSoundEnt = gameWorld.NewEntity();
        _shiftUnitSoundEnt = gameWorld.NewEntity();
        _truceSoundEnt = gameWorld.NewEntity();

        _inputEnt = gameWorld.NewEntity();
        _fromInfoEnt = gameWorld.NewEntity();





        SpawnAndFillCells(gameWorld);
        FillSelectorEnts();
        FillInfoEnts();


        var audioSourceParentGO = new GameObject("AudioSource");
        Instance.ECSmanager.EntitiesCommonManager.ToggleSceneParentGOZoneEnt_ParentGOZoneCom.AttachToCurrentParent(audioSourceParentGO.transform);

        MistakeAudioSource = audioSourceParentGO.AddComponent<AudioSource>();
        MistakeAudioSource.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.MistakeAudioClip;

        AttackAudioSource = audioSourceParentGO.AddComponent<AudioSource>();
        AttackAudioSource.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.AttackSwordAudioClip;


        var attackAS = audioSourceParentGO.AddComponent<AudioSource>();
        attackAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.AttackArcherAC;
        attackAS.volume = 0.6f;
        AttackArcherEnt_AudioSourceCom.StartFill(attackAS);


        var pickArcherAudioSource = audioSourceParentGO.AddComponent<AudioSource>();
        pickArcherAudioSource.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.PickArcherAudioClip;
        pickArcherAudioSource.volume = 0.7f;
        PickArcherEnt_AudioSourceCom.StartFill(pickArcherAudioSource);


        var pickMeleeAS = audioSourceParentGO.AddComponent<AudioSource>();
        pickMeleeAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.PickMeleeAC;
        pickMeleeAS.volume = 0.1f;
        PickMeleeEnt_AudioSourceCom.StartFill(pickMeleeAS);


        var buildingAS = audioSourceParentGO.AddComponent<AudioSource>();
        buildingAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.BuildingAC;
        buildingAS.volume = 0.1f;
        BuildingSoundEnt_AudioSourceCom.StartFill(buildingAS);


        var settingAS = audioSourceParentGO.AddComponent<AudioSource>();
        settingAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.SettingUnitAC;
        SettingSoundEnt_AudioSourceCom.StartFill(settingAS);


        var fireAS = audioSourceParentGO.AddComponent<AudioSource>();
        fireAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.FireAC;
        fireAS.volume = 0.2f;
        FireSoundEnt_AudioSourceCom.StartFill(fireAS);


        var buyAS = audioSourceParentGO.AddComponent<AudioSource>();
        buyAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.BuyAC;
        buyAS.volume = 0.3f;
        BuySoundEnt_AudioSourceCom.StartFill(buyAS);


        var meltingAS = audioSourceParentGO.AddComponent<AudioSource>();
        meltingAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.Melting_Clip;
        meltingAS.volume = 0.3f;
        MeltingSoundEnt_AudioSourceCom.StartFill(meltingAS);

        var destroyAS = audioSourceParentGO.AddComponent<AudioSource>();
        destroyAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.Destroy_Clip;
        destroyAS.volume = 0.3f;
        DestroySoundEnt_AudioSourceCom.StartFill(destroyAS);


        var upgradeUnitMeleeAS = audioSourceParentGO.AddComponent<AudioSource>();
        upgradeUnitMeleeAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.UpgradeUnitMelee_Clip;
        upgradeUnitMeleeAS.volume = 0.2f;
        UpgradeUnitMeleeSoundEnt_AudioSourceCom.StartFill(upgradeUnitMeleeAS);


        var seedingAS = audioSourceParentGO.AddComponent<AudioSource>();
        seedingAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.Seeding_Clip;
        seedingAS.volume = 0.2f;
        SeedingSoundEnt_AudioSourceCom.StartFill(seedingAS);


        var shiftUnitAS = audioSourceParentGO.AddComponent<AudioSource>();
        shiftUnitAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.ShiftUnit_Clip;
        shiftUnitAS.volume = 0.6f;
        ShiftUnitSoundEnt_AudioSourceCom.StartFill(shiftUnitAS);


        var truceAS = audioSourceParentGO.AddComponent<AudioSource>();
        truceAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.Truce_Clip;
        truceAS.volume = 0.6f;
        TruceSoundEnt_AudioSourceCom.StartFill(truceAS);




        BackGroundGO = GameObject.Instantiate(Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.PrefabConfig.BackGroundCollider2D,
        Instance.transform.position + new Vector3(7, 5.5f, 2), Instance.transform.rotation);

        Instance.ECSmanager.EntitiesCommonManager.ToggleSceneParentGOZoneEnt_ParentGOZoneCom.AttachToCurrentParent(BackGroundGO.transform);
        BackGroundSR = BackGroundGO.GetComponent<SpriteRenderer>();
        BackGroundSR.transform.rotation = Instance.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);


        SelectorEnt_SelectorCom.StartFill();
        SelectorEnt_RayCom.StartFill();
        SelectorEnt_UnitTypeCom.StartFill();
        SelectorEnt_UpgradeModTypeCom.StartFill();

        InputEnt_InputCom.StartFill();

        FromInfoEnt_FromInfoCom.StartFill();
    }

    private void SpawnAndFillCells(EcsWorld gameWorld)
    {
        var cellGO = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.PrefabConfig.CellGO;
        var whiteCellSR = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SpritesConfig.WhiteSprite;
        var blackCellSR = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SpritesConfig.BlackSprite;

        var cellsGO = new GameObject[CELL_COUNT_X, CELL_COUNT_Y];

        var supportParentForCells = new GameObject("Cells");
        Instance.ECSmanager.EntitiesCommonManager.ToggleSceneParentGOZoneEnt_ParentGOZoneCom.AttachToCurrentParent(supportParentForCells.transform);


        _cellProtectRelaxEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        _cellMaxStepsEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        _cellUnitEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

        _cellHpSupStatEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        _cellFertilizeSupStatEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        _cellForestSupStatEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        _cellOreSupStatEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

        _cellSupportVisionEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        _cellFireEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        _cellBuildingEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

        _cellFertilizerEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        _cellYoungForestEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        _cellAdultForestEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        _cellHillEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        _cellMountainEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

        _cellEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

        for (int x = 0; x < CELL_COUNT_X; x++)
            for (int y = 0; y < CELL_COUNT_Y; y++)
            {
                int[] xy = new int[] { x, y };

                if (y % 2 == 0)
                {
                    if (x % 2 == 0)
                    {
                        cellsGO[x, y] = CreateGameObject(cellGO, blackCellSR, x, y, Instance.gameObject);
                        SetActive(cellsGO[x, y], x, y);
                    }
                    if (x % 2 != 0)
                    {
                        cellsGO[x, y] = CreateGameObject(cellGO, whiteCellSR, x, y, Instance.gameObject);
                        SetActive(cellsGO[x, y], x, y);
                    }
                }
                if (y % 2 != 0)
                {
                    if (x % 2 != 0)
                    {
                        cellsGO[x, y] = CreateGameObject(cellGO, blackCellSR, x, y, Instance.gameObject);
                        SetActive(cellsGO[x, y], x, y);
                    }
                    if (x % 2 == 0)
                    {
                        cellsGO[x, y] = CreateGameObject(cellGO, whiteCellSR, x, y, Instance.gameObject);
                        SetActive(cellsGO[x, y], x, y);
                    }
                }


                var sr = cellsGO[x, y].transform.Find("ProtectRelax").GetComponent<SpriteRenderer>();
                _cellProtectRelaxEnts[x, y] = gameWorld.NewEntity()
                    .Replace(new SpriteRendererComponent(sr));


                sr = cellsGO[x, y].transform.Find("MaxSteps").GetComponent<SpriteRenderer>();
                _cellMaxStepsEnts[x, y] = gameWorld.NewEntity()
                    .Replace(new SpriteRendererComponent(sr));


                sr = cellsGO[x, y].transform.Find("Unit").GetComponent<SpriteRenderer>();
                _cellUnitEnts[x, y] = gameWorld.NewEntity()
                    .Replace(new CellUnitComponent())
                    .Replace(new UnitTypeComponent())
                    .Replace(new OwnerComponent())
                    .Replace(new OwnerBotComponent())
                    .Replace(new ActivatedDictComponent(new Dictionary<bool, bool>()))
                    .Replace(new ProtectRelaxComponent())
                    .Replace(new SpriteRendererComponent(sr));



                _cellHpSupStatEnts[x, y] = gameWorld.NewEntity();
                _cellFertilizeSupStatEnts[x, y] = gameWorld.NewEntity();
                _cellForestSupStatEnts[x, y] = gameWorld.NewEntity();
                _cellOreSupStatEnts[x, y] = gameWorld.NewEntity();

                _cellSupportVisionEnts[x, y] = gameWorld.NewEntity();
                _cellFireEnts[x, y] = gameWorld.NewEntity();
                _cellBuildingEnts[x, y] = gameWorld.NewEntity();

                _cellFertilizerEnts[x, y] = gameWorld.NewEntity();
                _cellYoungForestEnts[x, y] = gameWorld.NewEntity();
                _cellAdultForestEnts[x, y] = gameWorld.NewEntity();
                _cellHillEnts[x, y] = gameWorld.NewEntity();
                _cellMountainEnts[x, y] = gameWorld.NewEntity();

                _cellEnts[x, y] = gameWorld.NewEntity();










                var parentGO = cellsGO[x, y].transform.Find("SupportStatic").gameObject;
                CellHpSupStatEnt_SpriteRendererCom(xy).SpriteRenderer = parentGO.transform.Find("Hp").GetComponent<SpriteRenderer>();
                CellFertilizeSupStatEnt_SprRendCom(xy).SpriteRenderer = parentGO.transform.Find("Fertilizer").GetComponent<SpriteRenderer>();
                CellWoodSupStatEnt_SprRendCom(xy).SpriteRenderer = parentGO.transform.Find("Forest").GetComponent<SpriteRenderer>();
                CellOreSupStatEnt_SprRendCom(xy).SpriteRenderer = parentGO.transform.Find("Ore").GetComponent<SpriteRenderer>();


                parentGO = cellsGO[x, y].transform.Find("SupportVision").gameObject;
                CellSupVisEnt_SpriteRenderer(xy).SpriteRenderer = parentGO.GetComponent<SpriteRenderer>();


                parentGO = cellsGO[x, y].transform.Find("Fire").gameObject;
                CellFireEnt_SprRendCom(xy).SpriteRenderer = parentGO.GetComponent<SpriteRenderer>();
                CellFireEnt_HaverEffectCom(xy).StartFill();
                CellFireEnt_TimeStepsCom(xy).StartFill();


                parentGO = cellsGO[x, y].transform.Find("Buildings").gameObject;
                CellBuildEnt_CellBuilCom(x, y).StartFill();
                CellBuildEnt_BuilTypeCom(x, y).StartFill();
                CellBuildEnt_OwnerCom(x, y).StartFill();
                CellBuildEnt_OwnerBotCom(x, y).StartFill();
                CellBuildEnt_SpriteRendererCom(x, y).SpriteRenderer = parentGO.GetComponent<SpriteRenderer>();


                parentGO = cellsGO[x, y].transform.Find("Environments").gameObject;
                CellFertilizerEnt_SprRendCom(xy).SpriteRenderer = parentGO.transform.Find("Fertilizer").GetComponent<SpriteRenderer>();
                CellFertilizerEnt_AmountResourcesCom(xy).AmountResources = default;
                CellYoungForestEnt_SprRendCom(xy).SpriteRenderer = parentGO.transform.Find("YoungForest").GetComponent<SpriteRenderer>();
                CellAdultForestEnt_SprRendCom(xy).SpriteRenderer = parentGO.transform.Find("AdultForest").GetComponent<SpriteRenderer>();
                CellAdultForestEnt_AmountResourcesCom(xy).AmountResources = default;
                CellHillEnt_SprRendCom(xy).SpriteRenderer = parentGO.transform.Find("Hill").GetComponent<SpriteRenderer>();
                CellHillEnt_AmountResourcesCom(xy).AmountResources = default;
                CellMountainEnt_SprRendCom(xy).SpriteRenderer = parentGO.transform.Find("Mountain").GetComponent<SpriteRenderer>();


                var isStartedDict = new Dictionary<bool, bool>();
                isStartedDict[true] = y < 3 && x > 2 && x < 12;
                isStartedDict[false] = y > 7 && x > 2 && x < 12;

                cellsGO[x, y].transform.SetParent(supportParentForCells.transform);
                CellEnt_CellBaseCom(xy).StartFill(supportParentForCells, cellsGO[x, y].transform.Find("Cell").gameObject, isStartedDict);
                CellEnt_CellBaseCom(xy).Rotate();
                CellEnt_XyCellCom(xy).StartFill();




                if (Instance.IsMasterClient)
                {
                    int random;

                    if (y >= 4 && y <= 6)
                    {
                        random = Random.Range(1, 100);
                        if (random <= START_MOUNTAIN_PERCENT)
                            SetNewEnvironment(EnvironmentTypes.Mountain, xy);
                    }

                    if (!HaveEnvironment(EnvironmentTypes.Mountain, xy))
                    {
                        random = Random.Range(1, 100);
                        if (random <= START_FOREST_PERCENT)
                        {
                            SetNewEnvironment(EnvironmentTypes.AdultForest, xy);
                        }

                        if (!HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        {
                            random = Random.Range(1, 100);
                            if (random <= START_FERTILIZER_PERCENT)
                            {
                                SetNewEnvironment(EnvironmentTypes.Fertilizer, xy);
                            }
                        }


                        if (y >= 4 && y <= 6)
                        {
                            random = Random.Range(1, 100);
                            if (random <= START_HILL_PERCENT)
                                SetNewEnvironment(EnvironmentTypes.Hill, xy);

                        }
                    }
                }
            }


        if (PhotonNetwork.OfflineMode)
        {
            //int[] xy0 = new int[XY_FOR_ARRAY];
            //xy0[X] = 8;
            //xy0[Y] = 8;
            //var isSettedForest = false;
            //CellUnitWorker.SetBotUnit(UnitTypes.King, true, 300, 2, ProtectRelaxTypes.Relaxed, xy0);
            //CellEnvEnt_CellEnvCom(xy0).ResetAll();
            //var xyAround = CellUnitWorker.TryGetXYAround(xy0);

            //foreach (var xy1 in xyAround)
            //{
            //    CellUnitWorker.SetBotUnit(UnitTypes.Pawn, true, 150, 2, ProtectRelaxTypes.Relaxed, xy1);
            //    CellEnvEnt_CellEnvCom(xy1).ResetAll();

            //    if (!isSettedForest)
            //    {
            //        CellEnvEnt_CellEnvCom(xy1).SetNewEnvironment(EnvironmentTypes.AdultForest, xy1);
            //        isSettedForest = true;
            //    }
            //}

            //xy0[X] = 8;
            //xy0[Y] = 6;
            //CellBuildingWorker.SetBotBuilding(BuildingTypes.City, xy0);
            //CellEnvEnt_CellEnvCom(xy0).ResetAll();

            //int i = 0;
            //xyAround = CellUnitWorker.TryGetXYAround(xy0);
            //foreach (var xy1 in xyAround)
            //{
            //    CellUnitWorker.SetBotUnit(UnitTypes.PawnSword, true, 150, 2, ProtectRelaxTypes.Relaxed, xy1);
            //    CellEnvEnt_CellEnvCom(xy1).ResetAll();

            //    if (i == 0)
            //    {
            //        CellUnitWorker.ResetBotUnit(xy1);
            //        CellEnvEnt_CellEnvCom(xy1).SetNewEnvironment(EnvironmentTypes.Mountain, xy1);
            //    }

            //    else if (i == 3 || i == 4 || i == 6)
            //    {
            //        CellEnvEnt_CellEnvCom(xy1).SetNewEnvironment(EnvironmentTypes.AdultForest, xy1);
            //    }

            //    else if (i == 1)
            //    {
            //        CellEnvEnt_CellEnvCom(xy1).SetNewEnvironment(EnvironmentTypes.Hill, xy1);
            //    }


            //    i++;
            //}
        }



        GameObject CreateGameObject(GameObject go, Sprite sprite, int x, int y, GameObject mainGameGO2)
        {
            var goo = GameObject.Instantiate(go, mainGameGO2.transform.position + new Vector3(x, y, mainGameGO2.transform.position.z), mainGameGO2.transform.rotation);

            goo.name = "Cell";

            goo.transform.Find("Cell").GetComponent<SpriteRenderer>().sprite = sprite;

            return goo;
        }

        void SetActive(GameObject go, int x, int y)
        {
            if (x >= 0 && y == 0 || x >= 0 && y == 10 ||
                x == 1 && y >= 0 || x == 13 && y >= 0 ||
            x == 0 && y >= 0 || x == 14 && y >= 0 ||
            x == 1 && y == 1 || x == 2 && y == 1 || x == 12 && y == 1 || x == 13 && y == 1 ||
            x == 1 && y == 9 || x == 2 && y == 9 || x == 12 && y == 9 || x == 13 && y == 9)
                go.SetActive(false);
        }
    }

    private void FillSelectorEnts()
    {
        XyCurrentCellEnt_XyCellCom.StartFill();
        XySelectedCellEnt_XyCellCom.StartFill();
        XyPreviousCellEnt_XyCellCom.StartFill();
        XyPreviousVisionCellEnt_XyCellCom.StartFill();

        AvailableCellsSettingEnt_AvailCellsCom.StartFill();
        AvailableCellsShiftEnt_AvailCellsCom.StartFill();
        AvailableCellsSimpleAttackEnt_AvailCellsCom.StartFill();
        AvailableCellsUniqueAttackEnt_AvailCellsCom.StartFill();
    }

    private void FillInfoEnts()
    {
        KingInfoEnt_AmountUnitsInGameCom.StartFill();
        KingInfoEnt_AmountUnitsInInventorDictCom.StartFill();
        KingInfoEnt_IsSettedUnitDictCom.StartFill();
        KingInfoEnt_XySettedBuildingDictCom.StartFill();
        KingInfoEnt_AmountUnitsInRelaxCom.StartFill();

        PawnInfoEnt_AmountUnitsInGameCom.StartFill();
        PawnInfoEnt_AmountUnitsInInventorDictCom.StartFill();
        PawnInfoEnt_AmountUnitsInRelaxCom.StartFill();

        PawnSwordInfoEnt_AmountUnitsInGameCom.StartFill();
        PawnSwordInfoEnt_AmountUnitsInInventorDictCom.StartFill();
        PawnSwordInfoEnt_AmountUnitsInRelaxCom.StartFill();

        RookInfoEnt_AmountUnitsInGameCom.StartFill();
        RookInfoEnt_AmountUnitsInInventorDictCom.StartFill();
        RookInfoEnt_AmountUnitsInRelaxCom.StartFill();

        RookCrossbowInfoEnt_AmountUnitsInGameCom.StartFill();
        RookCrossbowInfoEnt_AmountUnitsInInventorDictCom.StartFill();
        RookCrossbowInfoEnt_AmountUnitsInRelaxCom.StartFill();

        BishopInfoEnt_AmountUnitsInGameCom.StartFill();
        BishopInfoEnt_AmountUnitsInInventorDictCom.StartFill();
        BishopInfoEnt_AmountUnitsInRelaxCom.StartFill();

        BishopCrossbowInfoEnt_AmountUnitsInGameCom.StartFill();
        BishopCrossbowInfoEnt_AmountUnitsInInventorDictCom.StartFill();
        BishopCrossbowInfoEnt_AmountUnitsInStandartCondition.StartFill();


        CityInfoEnt_AmountBuildingsInGameCom.StartFill();

        FarmsInfoEnt_AmountBuildingsInGameCom.StartFill();
        FarmsInfoEnt_AmountUpgradesCom.StartFill();

        WoodcuttersInfoEnt_AmountBuildingsInGameCom.StartFill();
        WoodcuttersInfoEnt_AmountUpgradesCom.StartFill();

        MinesInfoEnt_AmountBuildingsInGameCom.StartFill();
        MinesInfoEnt_AmountUpgradesCom.StartFill();


        FoodInfoEnt_AmountResourcesDictCom.StartFill();
        WoodInfoEnt_AmountResourcesDictCom.StartFill();
        OreInfoEnt_AmountResourcesDictCom.StartFill();
        IronInfoEnt_AmountResourcesDictCom.StartFill();
        GoldInfoEnt_AmountResourcesDictCom.StartFill();


        if (Instance.IsMasterClient)
        {
            InfoUnitsWorker.SetSettedKing(true, false);
            InfoUnitsWorker.SetSettedKing(false, false);

            InfoUnitsWorker.SetAmountUnitsInInventor(UnitTypes.King, true, EconomyValues.AMOUNT_KING_MASTER);
            InfoUnitsWorker.SetAmountUnitsInInventor(UnitTypes.King, false, EconomyValues.AMOUNT_KING_OTHER);

            InfoUnitsWorker.SetAmountUnitsInInventor(UnitTypes.Pawn, true, EconomyValues.AMOUNT_PAWN_MASTER);
            InfoUnitsWorker.SetAmountUnitsInInventor(UnitTypes.Pawn, false, EconomyValues.AMOUNT_PAWN_OTHER);

            InfoUnitsWorker.SetAmountUnitsInInventor(UnitTypes.Rook, true, EconomyValues.AMOUNT_ROOK_MASTER);
            InfoUnitsWorker.SetAmountUnitsInInventor(UnitTypes.Rook, false, EconomyValues.AMOUNT_ROOK_OTHER);

            InfoUnitsWorker.SetAmountUnitsInInventor(UnitTypes.Bishop, true, EconomyValues.AMOUNT_BISHOP_MASTER);
            InfoUnitsWorker.SetAmountUnitsInInventor(UnitTypes.Bishop, false, EconomyValues.AMOUNT_BISHOP_OTHER);


            InfoResourcesWorker.SetAmountResources(ResourceTypes.Food, true, EconomyValues.AMOUNT_FOOD_MASTER);
            InfoResourcesWorker.SetAmountResources(ResourceTypes.Wood, true, EconomyValues.AMOUNT_WOOD_MASTER);
            InfoResourcesWorker.SetAmountResources(ResourceTypes.Ore, true, EconomyValues.AMOUNT_ORE_MASTER);
            InfoResourcesWorker.SetAmountResources(ResourceTypes.Iron, true, EconomyValues.AMOUNT_IRON_MASTER);
            InfoResourcesWorker.SetAmountResources(ResourceTypes.Gold, true, EconomyValues.AMOUNT_GOLD_MASTER);

            InfoResourcesWorker.SetAmountResources(ResourceTypes.Food, false, EconomyValues.AMOUNT_FOOD_OTHER);
            InfoResourcesWorker.SetAmountResources(ResourceTypes.Wood, false, EconomyValues.AMOUNT_WOOD_OTHER);
            InfoResourcesWorker.SetAmountResources(ResourceTypes.Ore, false, EconomyValues.AMOUNT_ORE_OTHER);
            InfoResourcesWorker.SetAmountResources(ResourceTypes.Iron, false, EconomyValues.AMOUNT_IRON_OTHER);
            InfoResourcesWorker.SetAmountResources(ResourceTypes.Gold, false, EconomyValues.AMOUNT_GOLD_OTHER);
        }
    }
}