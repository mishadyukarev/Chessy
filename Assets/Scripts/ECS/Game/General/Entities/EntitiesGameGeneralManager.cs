using Assets.Scripts;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.Components;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.ECS.Game.General.Entities;
using Assets.Scripts.ECS.Game.General.Entities.Containers;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Game.Else;
using Assets.Scripts.Workers.Game.Else.Cell;
using Assets.Scripts.Workers.Game.Else.Fire;
using Assets.Scripts.Workers.Info;
using ExitGames.Client.Photon.StructWrapping;
using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;
using static Assets.Scripts.CellEnvironmentWorker;
using static Assets.Scripts.Main;

public sealed class EntitiesGameGeneralManager : EntitiesManager
{
    internal GameObject BackGroundGO;
    internal SpriteRenderer BackGroundSR;
    internal AudioSource MistakeAudioSource;
    internal AudioSource AttackAudioSource;


    #region Cells

    private CellSupVisBlocksEntsContainer _cellSupVisBlocksContainer;
    private CellUnitEntsContainer _cellUnitEntsContainer;
    private CellSupVisBarsEntsContainer _cellSupVisBarsContainer;
    private CellSupVisEntsContainer _cellSupVisEntsContainer;
    private CellFireEntsContainer _cellFireEntsContainer;
    private CellBuildingEntsContainer _cellBuildingEntsContainer;
    private CellEnvironmentEntsContainer _cellEnvironmentEntsContainer;
    private CellEntsContainer _cellContainer;

    internal int Xamount => _cellContainer.Xamount;
    internal int Yamount => _cellContainer.Yamount;

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


    private AvailableCellEntsContainer _availableCellEntsContainer;

    #endregion


    #region Info

    #region Cells

    private EcsEntity _cellsInfoEnt;
    internal ref XyStartCellsComponent CellsInfoEnt_XyStartCellsCom => ref _cellsInfoEnt.Get<XyStartCellsComponent>();

    #endregion


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
        FillInfoEnts(gameWorld);
        FillSelectorEnts(gameWorld);


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


        var cellProtectRelaxEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        var cellMaxStepsEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        var cellUnitEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

        var cellHpSupStatEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        var cellFertilizeSupStatEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        var cellForestSupStatEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        var cellOreSupStatEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

        var cellSupportVisionEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        var cellFireEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        var cellBuildingEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

        var cellFertilizerEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        var cellYoungForestEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        var cellAdultForestEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        var cellHillEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        var cellMountainEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

        var cellEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

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

                cellsGO[x, y].transform.SetParent(supportParentForCells.transform);



                var sr = cellsGO[x, y].transform.Find("ProtectRelax").GetComponent<SpriteRenderer>();
                cellProtectRelaxEnts[x, y] = gameWorld.NewEntity()
                    .Replace(new SpriteRendererComponent(sr));



                sr = cellsGO[x, y].transform.Find("MaxSteps").GetComponent<SpriteRenderer>();
                cellMaxStepsEnts[x, y] = gameWorld.NewEntity()
                    .Replace(new SpriteRendererComponent(sr));



                sr = cellsGO[x, y].transform.Find("Unit").GetComponent<SpriteRenderer>();
                cellUnitEnts[x, y] = gameWorld.NewEntity()
                    .Replace(new CellUnitComponent())
                    .Replace(new UnitTypeComponent())
                    .Replace(new OwnerComponent())
                    .Replace(new OwnerBotComponent())
                    .Replace(new IsVisibleDictComponent(new Dictionary<bool, bool>()))
                    .Replace(new ProtectRelaxComponent())
                    .Replace(new SpriteRendererComponent(sr));



                var parentGO = cellsGO[x, y].transform.Find("SupportStatic").gameObject;

                sr = parentGO.transform.Find("Hp").GetComponent<SpriteRenderer>();
                cellHpSupStatEnts[x, y] = gameWorld.NewEntity()
                    .Replace(new SpriteRendererComponent(sr));

                sr = parentGO.transform.Find("Fertilizer").GetComponent<SpriteRenderer>();
                cellFertilizeSupStatEnts[x, y] = gameWorld.NewEntity()
                    .Replace(new SpriteRendererComponent(sr));

                sr = parentGO.transform.Find("Forest").GetComponent<SpriteRenderer>();
                cellForestSupStatEnts[x, y] = gameWorld.NewEntity()
                    .Replace(new SpriteRendererComponent(sr));

                sr = parentGO.transform.Find("Ore").GetComponent<SpriteRenderer>();
                cellOreSupStatEnts[x, y] = gameWorld.NewEntity()
                    .Replace(new SpriteRendererComponent(sr));



                parentGO = cellsGO[x, y].transform.Find("SupportVision").gameObject;

                sr = parentGO.GetComponent<SpriteRenderer>();
                cellSupportVisionEnts[x, y] = gameWorld.NewEntity()
                    .Replace(new SpriteRendererComponent(sr));



                parentGO = cellsGO[x, y].transform.Find("Fire").gameObject;

                sr = parentGO.GetComponent<SpriteRenderer>();
                cellFireEnts[x, y] = gameWorld.NewEntity()
                    .Replace(new SpriteRendererComponent(sr))
                    .Replace(new HaverEffectComponent())
                    .Replace(new TimeStepsComponent());



                parentGO = cellsGO[x, y].transform.Find("Buildings").gameObject;

                sr = parentGO.GetComponent<SpriteRenderer>();


                cellBuildingEnts[x, y] = gameWorld.NewEntity()
                    .Replace(new SpriteRendererComponent(sr))
                    .Replace(new CellBuildingComponent())
                    .Replace(new BuildingTypeComponent())
                    .Replace(new OwnerComponent())
                    .Replace(new OwnerBotComponent());



                parentGO = cellsGO[x, y].transform.Find("Environments").gameObject;

                sr = parentGO.transform.Find("Fertilizer").GetComponent<SpriteRenderer>();

                cellFertilizerEnts[x, y] = gameWorld.NewEntity()
                    .Replace(new SpriteRendererComponent(sr))
                    .Replace(new AmountResourcesComponent());



                sr = parentGO.transform.Find("YoungForest").GetComponent<SpriteRenderer>();

                cellYoungForestEnts[x, y] = gameWorld.NewEntity()
                    .Replace(new SpriteRendererComponent(sr));



                sr = parentGO.transform.Find("AdultForest").GetComponent<SpriteRenderer>();

                cellAdultForestEnts[x, y] = gameWorld.NewEntity()
                    .Replace(new SpriteRendererComponent(sr))
                    .Replace(new AmountResourcesComponent());



                sr = parentGO.transform.Find("Hill").GetComponent<SpriteRenderer>();

                cellHillEnts[x, y] = gameWorld.NewEntity()
                    .Replace(new SpriteRendererComponent(sr))
                    .Replace(new AmountResourcesComponent());



                sr = parentGO.transform.Find("Mountain").GetComponent<SpriteRenderer>();

                cellMountainEnts[x, y] = gameWorld.NewEntity()
                    .Replace(new SpriteRendererComponent(sr))
                    .Replace(new AmountResourcesComponent());


                parentGO = cellsGO[x, y].transform.Find("Cell").gameObject;

                cellEnts[x, y] = gameWorld.NewEntity()
                    .Replace(new CellGOComponent(parentGO));

                parentGO.transform.rotation = Instance.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);
            }

        _cellSupVisBarsContainer = new CellSupVisBarsEntsContainer((cellHpSupStatEnts, cellFertilizeSupStatEnts, cellForestSupStatEnts, cellOreSupStatEnts));
        new CellSupVisBarsWorker(_cellSupVisBarsContainer);

        _cellSupVisBlocksContainer = new CellSupVisBlocksEntsContainer((cellProtectRelaxEnts, cellMaxStepsEnts));
        new CellSupVisBlocksWorker(_cellSupVisBlocksContainer);

        _cellSupVisEntsContainer = new CellSupVisEntsContainer(cellSupportVisionEnts);
        new CellSupVisWorker(_cellSupVisEntsContainer);

        _cellFireEntsContainer = new CellFireEntsContainer(cellFireEnts);
        new CellFireWorker(_cellFireEntsContainer);

        _cellUnitEntsContainer = new CellUnitEntsContainer(cellUnitEnts);
        new CellUnitWorker(_cellUnitEntsContainer);

        _cellBuildingEntsContainer = new CellBuildingEntsContainer(cellBuildingEnts);
        new CellBuildingWorker(_cellBuildingEntsContainer);

        _cellEnvironmentEntsContainer = new CellEnvironmentEntsContainer((cellFertilizerEnts, cellYoungForestEnts, cellAdultForestEnts, cellHillEnts, cellMountainEnts));
        new CellEnvironmentWorker(_cellEnvironmentEntsContainer);

        _cellContainer = new CellEntsContainer(cellEnts);
        new CellWorker(_cellContainer);

        for (int x = 0; x < CELL_COUNT_X; x++)
            for (int y = 0; y < CELL_COUNT_Y; y++)
            {
                var xy = new int[] { x, y };

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

    private void FillSelectorEnts(EcsWorld gameWorld)
    {
        XyCurrentCellEnt_XyCellCom.StartFill();
        XySelectedCellEnt_XyCellCom.StartFill();
        XyPreviousCellEnt_XyCellCom.StartFill();
        XyPreviousVisionCellEnt_XyCellCom.StartFill();

        var availableCellsSettingEnt = gameWorld.NewEntity()
            .Replace(new AvailableCellsComponent(new List<int[]>()));
        var availableCellsShiftEnt = gameWorld.NewEntity()
            .Replace(new AvailableCellsComponent(new List<int[]>()));
        var availableCellsSimpleAttackEnt = gameWorld.NewEntity()
            .Replace(new AvailableCellsComponent(new List<int[]>()));
        var availableCellsUniqueAttackEnt = gameWorld.NewEntity()
            .Replace(new AvailableCellsComponent(new List<int[]>()));

        _availableCellEntsContainer = new AvailableCellEntsContainer
            ((availableCellsSettingEnt, availableCellsShiftEnt, availableCellsSimpleAttackEnt, availableCellsUniqueAttackEnt));

        new AvailableCellsEntsWorker(_availableCellEntsContainer);
    }

    private void FillInfoEnts(EcsWorld gameWorld)
    {
        var listMaster = new List<int[]>();
        var listOther = new List<int[]>();

        for (int x = 0; x < Xamount; x++)
            for (int y = 0; y < Yamount; y++)
            {
                if (y < 3 && x > 2 && x < 12)
                {
                    listMaster.Add(new int[] { x, y });
                }
                else if (y > 7 && x > 2 && x < 12)
                {
                    listOther.Add(new int[] { x, y });
                }
            }
        var dict = new Dictionary<bool, List<int[]>>();
        dict.Add(true, listMaster);
        dict.Add(false, listOther);

        _cellsInfoEnt = gameWorld.NewEntity()
            .Replace(new XyStartCellsComponent(dict));




        _kingInfoEnt = gameWorld.NewEntity()
            .Replace(new AmountUnitsInGameComponent(new Dictionary<bool, List<int[]>>()));
        KingInfoEnt_AmountUnitsInInventorDictCom.StartFill();
        KingInfoEnt_IsSettedUnitDictCom.StartFill();
        KingInfoEnt_XySettedBuildingDictCom.StartFill();
        KingInfoEnt_AmountUnitsInRelaxCom.StartFill();


        _pawnInfoEnt = gameWorld.NewEntity()
            .Replace(new AmountUnitsInGameComponent(new Dictionary<bool, List<int[]>>()));
        PawnInfoEnt_AmountUnitsInInventorDictCom.StartFill();
        PawnInfoEnt_AmountUnitsInRelaxCom.StartFill();

        _pawnSwordInfoEnt = gameWorld.NewEntity()
            .Replace(new AmountUnitsInGameComponent(new Dictionary<bool, List<int[]>>()));
        PawnSwordInfoEnt_AmountUnitsInInventorDictCom.StartFill();
        PawnSwordInfoEnt_AmountUnitsInRelaxCom.StartFill();

        _rookInfoEnt = gameWorld.NewEntity()
            .Replace(new AmountUnitsInGameComponent(new Dictionary<bool, List<int[]>>()));
        RookInfoEnt_AmountUnitsInInventorDictCom.StartFill();
        RookInfoEnt_AmountUnitsInRelaxCom.StartFill();

        _rookCrossbowInfoEnt = gameWorld.NewEntity()
            .Replace(new AmountUnitsInGameComponent(new Dictionary<bool, List<int[]>>()));
        RookCrossbowInfoEnt_AmountUnitsInInventorDictCom.StartFill();
        RookCrossbowInfoEnt_AmountUnitsInRelaxCom.StartFill();

        _bishopInfoInGameEnt = gameWorld.NewEntity()
            .Replace(new AmountUnitsInGameComponent(new Dictionary<bool, List<int[]>>()));
        BishopInfoEnt_AmountUnitsInInventorDictCom.StartFill();
        BishopInfoEnt_AmountUnitsInRelaxCom.StartFill();

        _bishopCrossbowInfoEnt = gameWorld.NewEntity()
            .Replace(new AmountUnitsInGameComponent(new Dictionary<bool, List<int[]>>()));
        BishopCrossbowInfoEnt_AmountUnitsInInventorDictCom.StartFill();
        BishopCrossbowInfoEnt_AmountUnitsInStandartCondition.StartFill();


        _cityInfoEnt = gameWorld.NewEntity();
        _farmsInfoEnt = gameWorld.NewEntity();
        _woodcuttersInfoEnt = gameWorld.NewEntity();
        _minesInfoEnt = gameWorld.NewEntity();

        _foodInfoEnt = gameWorld.NewEntity();
        _woodInfoEnt = gameWorld.NewEntity();
        _oreInfoEnt = gameWorld.NewEntity();
        _ironInfoEnt = gameWorld.NewEntity();
        _goldInfoEnt = gameWorld.NewEntity();









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