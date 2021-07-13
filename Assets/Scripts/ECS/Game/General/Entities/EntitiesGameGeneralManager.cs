using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Game.Components;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.Static;
using ExitGames.Client.Photon.StructWrapping;
using Leopotam.Ecs;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Abstractions.EnvironmentValues;
using static Assets.Scripts.Abstractions.ValuesConst;
using static Assets.Scripts.Main;

public sealed partial class EntitiesGameGeneralManager : IDisposable
{
    internal GameObject BackGroundGO;
    internal SpriteRenderer BackGroundSR;
    internal AudioSource MistakeAudioSource;
    internal AudioSource AttackAudioSource;


    #region Cells

    private EcsEntity[,] _cellEnts;
    private EcsEntity[,] _cellUnitEnts;
    private EcsEntity[,] _cellBuildingEnts;
    private EcsEntity[,] _cellEnvironmentEnts;
    private EcsEntity[,] _cellSupportVisionEnts;
    private EcsEntity[,] _cellSupportStaticEnts;
    private EcsEntity[,] _cellEffectEnts;

    internal ref CellBaseComponent CellEnt_CellBaseCom(params int[] xy) => ref _cellEnts[xy[X], xy[Y]].Get<CellBaseComponent>();
    internal ref CellComponent CellEnt_CellCom(params int[] xy) => ref _cellEnts[xy[X], xy[Y]].Get<CellComponent>();

    internal ref CellUnitComponent CellUnitEnt_CellUnitCom(params int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>();
    internal ref OwnerComponent CellUnitEnt_CellOwnerCom(params int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<OwnerComponent>();
    internal ref OwnerBotComponent CellUnitEnt_CellOwnerBotCom(params int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<OwnerBotComponent>();
    internal ref UnitTypeComponent CellUnitEnt_UnitTypeCom(params int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<UnitTypeComponent>();
    internal ref ActivatedDictComponent CellUnitEnt_ActivatedForPlayersCom(params int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<ActivatedDictComponent>();
    internal ref ProtectRelaxComponent CellUnitEnt_ProtectRelaxCom(params int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<ProtectRelaxComponent>();

    internal ref CellEnvironmentComponent CellEnvEnt_CellEnvCom(params int[] xy) => ref _cellEnvironmentEnts[xy[X], xy[Y]].Get<CellEnvironmentComponent>();

    internal ref CellBuildingComponent CellBuildEnt_CellBuilCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<CellBuildingComponent>();
    internal ref BuildingTypeComponent CellBuildEnt_BuilTypeCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<BuildingTypeComponent>();
    internal ref CellComponent CellBuildEnt_CellCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<CellComponent>();
    internal ref OwnerComponent CellBuildEnt_OwnerCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<OwnerComponent>();
    internal ref OwnerBotComponent CellBuildEnt_CellOwnerBotCom(params int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<OwnerBotComponent>();

    internal ref CellSupportVisionComponent CellSupVisEnt_CellSupVisCom(params int[] xy) => ref _cellSupportVisionEnts[xy[X], xy[Y]].Get<CellSupportVisionComponent>();

    internal ref CellSupportStaticComponent CellSupStatEnt_CellSupStatCom(params int[] xy) => ref _cellSupportStaticEnts[xy[X], xy[Y]].Get<CellSupportStaticComponent>();

    internal ref CellEffectComponent CellEffectEnt_CellEffectCom(params int[] xy) => ref _cellEffectEnts[xy[X], xy[Y]].Get<CellEffectComponent>();

    internal int Xamount => _cellEnts.GetUpperBound(X) + 1;
    internal int Yamount => _cellEnts.GetUpperBound(Y) + 1;

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


    private EcsEntity _buildingsEnt;
    internal ref BuildingsComponent BuildingsEnt_BuildingsCom => ref _buildingsEnt.Get<BuildingsComponent>();
    internal ref UpgradeBuildingsComponent BuildingsEnt_UpgradeBuildingsCom => ref _buildingsEnt.Get<UpgradeBuildingsComponent>();


    private EcsEntity _unitInfoEnt;
    internal ref UnitInventorComponent UnitInfoEnt_UnitInventorCom => ref _unitInfoEnt.Get<UnitInventorComponent>();


    private EcsEntity _inputEnt;
    internal ref InputComponent InputEnt_InputCom => ref _inputEnt.Get<InputComponent>();


    private EcsEntity _rPCGeneralEntity;
    internal ref RpcComponent RpcGeneralEnt_RPCCom => ref _rPCGeneralEntity.Get<RpcComponent>();


    private EcsEntity _fromInfoEnt;
    internal ref FromInfoComponent FromInfoEnt_FromInfoCom => ref _fromInfoEnt.Get<FromInfoComponent>();


    private EcsEntity _selectorEnt;
    internal ref SelectorComponent SelectorEnt_SelectorCom => ref _selectorEnt.Get<SelectorComponent>();
    internal ref RaycastHit2DComponent SelectorEnt_RayCom => ref _selectorEnt.Get<RaycastHit2DComponent>();
    internal ref UnitTypeComponent SelectorEnt_UnitTypeCom => ref _selectorEnt.Get<UnitTypeComponent>();


    private EcsEntity _soundEnt;
    internal ref SounddComponent SoundEnt_SoundCom => ref _soundEnt.Get<SounddComponent>();


    private EcsEntity _zoneEnt;
    internal ref ZoneComponent ZoneEnt_ZoneCom => ref _zoneEnt.Get<ZoneComponent>();


    private EcsEntity _animationEnt;
    internal ref AnimationAttackUnitComponent AnimationAttack_UnitComponent => ref _animationEnt.Get<AnimationAttackUnitComponent>();


    #region UnitInfoInGame

    private EcsEntity _kingInfoInGameEnt;
    internal ref AmountDictionaryComponent KingInfoInGameEnt_AmountDictCom => ref _kingInfoInGameEnt.Get<AmountDictionaryComponent>();


    private EcsEntity _pawnInfoInGameEnt;
    internal ref AmountDictionaryComponent PawnInfoInGameEnt_AmountDictCom => ref _pawnInfoInGameEnt.Get<AmountDictionaryComponent>();


    private EcsEntity _pawnSwordInfoInGameEnt;
    internal ref AmountDictionaryComponent PawnSwordInfoInGameEnt_AmountDictCom => ref _pawnSwordInfoInGameEnt.Get<AmountDictionaryComponent>();


    private EcsEntity _rookInfoInGameEnt;
    internal ref AmountDictionaryComponent RookInfoInGameEnt_AmountDictCom => ref _rookInfoInGameEnt.Get<AmountDictionaryComponent>();


    private EcsEntity _rookCrossbowInfoInGameEnt;
    internal ref AmountDictionaryComponent RookCrossbowInfoInGameEnt_AmountDictCom => ref _rookCrossbowInfoInGameEnt.Get<AmountDictionaryComponent>();


    private EcsEntity _bishopInfoInGameEnt;
    internal ref AmountDictionaryComponent BishopInfoInGameEnt_AmountDictCom => ref _bishopInfoInGameEnt.Get<AmountDictionaryComponent>();


    private EcsEntity _bishopCrossbowInfoInGameEnt;
    internal ref AmountDictionaryComponent BishopCrossbowInfoInGameEnt_AmountDictCom => ref _bishopCrossbowInfoInGameEnt.Get<AmountDictionaryComponent>();

    #endregion



    internal EntitiesGameGeneralManager(EcsWorld gameWorld)
    {
        _cellEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        _cellUnitEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        _cellBuildingEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        _cellEnvironmentEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        _cellSupportVisionEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        _cellSupportStaticEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
        _cellEffectEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

        for (int x = 0; x < CELL_COUNT_X; x++)
            for (int y = 0; y < CELL_COUNT_Y; y++)
            {
                _cellEnts[x, y] = gameWorld.NewEntity();
                _cellUnitEnts[x, y] = gameWorld.NewEntity();
                _cellBuildingEnts[x, y] = gameWorld.NewEntity();
                _cellEnvironmentEnts[x, y] = gameWorld.NewEntity();
                _cellSupportVisionEnts[x, y] = gameWorld.NewEntity();
                _cellSupportStaticEnts[x, y] = gameWorld.NewEntity();
                _cellEffectEnts[x, y] = gameWorld.NewEntity();
            }

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

        _economyEnt = gameWorld.NewEntity();
        _unitInfoEnt = gameWorld.NewEntity();
        _buildingsEnt = gameWorld.NewEntity();
        _selectorEnt = gameWorld.NewEntity();
        _rPCGeneralEntity = gameWorld.NewEntity();
        _inputEnt = gameWorld.NewEntity();
        _soundEnt = gameWorld.NewEntity();
        _animationEnt = gameWorld.NewEntity();
        _zoneEnt = gameWorld.NewEntity();
        _fromInfoEnt = gameWorld.NewEntity();

        _kingInfoInGameEnt = gameWorld.NewEntity();
        _pawnInfoInGameEnt = gameWorld.NewEntity();
        _pawnSwordInfoInGameEnt = gameWorld.NewEntity();
        _rookInfoInGameEnt = gameWorld.NewEntity();
        _rookCrossbowInfoInGameEnt = gameWorld.NewEntity();
        _bishopInfoInGameEnt = gameWorld.NewEntity();
        _bishopCrossbowInfoInGameEnt = gameWorld.NewEntity();


        ConstructCanvast(gameWorld);
    }


    internal void FillEntities(ResourcesCommComponent resourcesCommComponent)
    {
        SpawnCells();

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



        EconomyEnt_EconomyCom.StartFill();
        EconomyUIEnt_EconomyUICom.StartFill();
        EconomyEnt_MistakeEconomyCom.StartFill();

        BuildingsEnt_BuildingsCom.StartFill();
        BuildingsEnt_UpgradeBuildingsCom.StartFill();

        UnitInfoEnt_UnitInventorCom.StartFill();

        SelectorEnt_SelectorCom.StartFill();
        SelectorEnt_RayCom.StartFill();
        SelectorEnt_UnitTypeCom.StartFill();

        RpcGeneralEnt_RPCCom.StartFill();

        InputEnt_InputCom.StartFill();

        SoundEnt_SoundCom.StartFill();

        AnimationAttack_UnitComponent.StartFill();

        ZoneEnt_ZoneCom.StartFill();

        FromInfoEnt_FromInfoCom.StartFill();


        KingInfoInGameEnt_AmountDictCom.StartFill();
        PawnInfoInGameEnt_AmountDictCom.StartFill();
        PawnSwordInfoInGameEnt_AmountDictCom.StartFill();
        RookInfoInGameEnt_AmountDictCom.StartFill();
        RookCrossbowInfoInGameEnt_AmountDictCom.StartFill();
        BishopInfoInGameEnt_AmountDictCom.StartFill();
        BishopCrossbowInfoInGameEnt_AmountDictCom.StartFill();


        if (Instance.IsMasterClient)
        {
            #region Info

            UnitInfoEnt_UnitInventorCom.SetSettedKing(true, false);
            UnitInfoEnt_UnitInventorCom.SetSettedKing(false, false);

            UnitInfoEnt_UnitInventorCom.SetAmountUnitsInInventor(UnitTypes.King, true, resourcesCommComponent.StartValuesGameConfig.AMOUNT_KING_MASTER);
            UnitInfoEnt_UnitInventorCom.SetAmountUnitsInInventor(UnitTypes.King, false, resourcesCommComponent.StartValuesGameConfig.AMOUNT_KING_OTHER);

            UnitInfoEnt_UnitInventorCom.SetAmountUnitsInInventor(UnitTypes.Pawn, true, resourcesCommComponent.StartValuesGameConfig.AMOUNT_PAWN_MASTER);
            UnitInfoEnt_UnitInventorCom.SetAmountUnitsInInventor(UnitTypes.Pawn, false, resourcesCommComponent.StartValuesGameConfig.AMOUNT_PAWN_OTHER);

            UnitInfoEnt_UnitInventorCom.SetAmountUnitsInInventor(UnitTypes.Rook, true, resourcesCommComponent.StartValuesGameConfig.AMOUNT_ROOK_MASTER);
            UnitInfoEnt_UnitInventorCom.SetAmountUnitsInInventor(UnitTypes.Rook, false, resourcesCommComponent.StartValuesGameConfig.AMOUNT_ROOK_OTHER);

            UnitInfoEnt_UnitInventorCom.SetAmountUnitsInInventor(UnitTypes.Bishop, true, resourcesCommComponent.StartValuesGameConfig.AMOUNT_BISHOP_MASTER);
            UnitInfoEnt_UnitInventorCom.SetAmountUnitsInInventor(UnitTypes.Bishop, false, resourcesCommComponent.StartValuesGameConfig.AMOUNT_BISHOP_OTHER);

            BuildingsEnt_BuildingsCom.IsSettedCityDict[true] = default;
            BuildingsEnt_BuildingsCom.IsSettedCityDict[false] = default;

            BuildingsEnt_BuildingsCom.SetAmountBuildings(BuildingTypes.Farm, true, default);
            BuildingsEnt_BuildingsCom.SetAmountBuildings(BuildingTypes.Farm, false, default);

            BuildingsEnt_BuildingsCom.SetAmountBuildings(BuildingTypes.Woodcutter, true, default);
            BuildingsEnt_BuildingsCom.SetAmountBuildings(BuildingTypes.Woodcutter, false, default);

            BuildingsEnt_BuildingsCom.SetAmountBuildings(BuildingTypes.Mine, true, default);
            BuildingsEnt_BuildingsCom.SetAmountBuildings(BuildingTypes.Mine, false, default);

            #endregion


            #region Economy
            EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Food, true, resourcesCommComponent.StartValuesGameConfig.AMOUNT_FOOD_MASTER);
            EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Wood, true, resourcesCommComponent.StartValuesGameConfig.AMOUNT_WOOD_MASTER);
            EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Ore, true, resourcesCommComponent.StartValuesGameConfig.AMOUNT_ORE_MASTER);
            EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Iron, true, resourcesCommComponent.StartValuesGameConfig.AMOUNT_IRON_MASTER);
            EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Gold, true, resourcesCommComponent.StartValuesGameConfig.AMOUNT_GOLD_MASTER);

            EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Food, false, resourcesCommComponent.StartValuesGameConfig.AMOUNT_FOOD_OTHER);
            EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Wood, false, resourcesCommComponent.StartValuesGameConfig.AMOUNT_WOOD_OTHER);
            EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Ore, false, resourcesCommComponent.StartValuesGameConfig.AMOUNT_ORE_OTHER);
            EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Iron, false, resourcesCommComponent.StartValuesGameConfig.AMOUNT_IRON_OTHER);
            EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Gold, false, resourcesCommComponent.StartValuesGameConfig.AMOUNT_GOLD_OTHER);

            #endregion
        }

        SpawnAndFillCanvasEntities();
    }

    private void SpawnCells()
    {
        var cellGO = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.PrefabConfig.CellGO;
        var whiteCellSR = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SpritesConfig.WhiteSprite;
        var blackCellSR = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SpritesConfig.BlackSprite;

        var cellsGO = new GameObject[CELL_COUNT_X, CELL_COUNT_Y];

        var supportParentForCells = new GameObject("Cells");
        Instance.ECSmanager.EntitiesCommonManager.ToggleSceneParentGOZoneEnt_ParentGOZoneCom.AttachToCurrentParent(supportParentForCells.transform);


        for (int x = 0; x < CELL_COUNT_X; x++)
            for (int y = 0; y < CELL_COUNT_Y; y++)
            {
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

                var isStartedDict = new Dictionary<bool, bool>();
                isStartedDict[true] = y < 3 && x > 2 && x < 12;
                isStartedDict[false] = y > 7 && x > 2 && x < 12;

                cellsGO[x, y].transform.SetParent(supportParentForCells.transform);
                CellEnt_CellBaseCom(x, y).StartFill(supportParentForCells, cellsGO[x, y], isStartedDict);
                CellEnt_CellBaseCom(x, y).RotateAndFixRot();
                CellEnt_CellCom(x, y).StartFill();


                GameObject parentGO = cellsGO[x, y].transform.Find("Environments").gameObject;
                CellEnvEnt_CellEnvCom(x, y).StartFill(parentGO);


                parentGO = cellsGO[x, y].transform.Find("SupportVisions").gameObject;
                CellSupVisEnt_CellSupVisCom(x, y).Fill(parentGO);


                parentGO = cellsGO[x, y].transform.Find("SupportStatic").gameObject;
                CellSupStatEnt_CellSupStatCom(x, y).Fill(parentGO);


                parentGO = cellsGO[x, y].transform.Find("Units").gameObject;
                CellUnitEnt_CellUnitCom(x, y).StartFill(parentGO);
                CellUnitEnt_UnitTypeCom(x, y).StartFill();
                CellUnitEnt_CellOwnerCom(x, y).StartFill();
                CellUnitEnt_CellOwnerBotCom(x, y).StartFill();
                CellUnitEnt_ActivatedForPlayersCom(x, y).StartFill();
                CellUnitEnt_ProtectRelaxCom(x, y).StartFill();


                parentGO = cellsGO[x, y].transform.Find("Buildings").gameObject;
                CellBuildEnt_CellBuilCom(x, y).StartFill(parentGO);
                CellBuildEnt_BuilTypeCom(x, y).StartFill();
                CellBuildEnt_OwnerCom(x, y).StartFill();
                CellBuildEnt_CellOwnerBotCom(x, y).StartFill();


                parentGO = cellsGO[x, y].transform.Find("Effects").gameObject;
                CellEffectEnt_CellEffectCom(x, y).StartFill(parentGO);



                if (Instance.IsMasterClient)
                {
                    int random;

                    if (y >= 4 && y <= 6)
                    {
                        random = UnityEngine.Random.Range(1, 100);
                        if (random <= MOUNTAIN_PERCENT)
                            CellEnvEnt_CellEnvCom(x, y).SetNewEnvironment(EnvironmentTypes.Mountain);
                    }

                    if (!CellEnvEnt_CellEnvCom(x, y).HaveEnvironment(EnvironmentTypes.Mountain))
                    {
                        random = UnityEngine.Random.Range(1, 100);
                        if (random <= FOREST_PERCENT)
                        {
                            CellEnvEnt_CellEnvCom(x, y).SetNewEnvironment(EnvironmentTypes.AdultForest);
                        }

                        if (!CellEnvEnt_CellEnvCom(x, y).HaveEnvironment(EnvironmentTypes.AdultForest))
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= FERTILIZER_PERCENT)
                            {
                                CellEnvEnt_CellEnvCom(x, y).SetNewEnvironment(EnvironmentTypes.Fertilizer);
                            }
                        }


                        if (y >= 4 && y <= 6)
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= HILL_PERCENT)
                                CellEnvEnt_CellEnvCom(x, y).SetNewEnvironment(EnvironmentTypes.Hill);

                        }
                    }
                }
            }


        if (PhotonNetwork.OfflineMode/*Instance.GameModeType == GameModTypes.WithBot*/)
        {
            int[] xy0 = new int[XY_FOR_ARRAY];
            xy0[X] = 8;
            xy0[Y] = 8;
            var isSettedForest = false;
            CellUnitWorker.SetBotUnit(UnitTypes.King, true, 300, 2, ProtectRelaxTypes.Relaxed, xy0);
            CellEnvEnt_CellEnvCom(xy0).ResetAll();
            var xyAround = CellUnitWorker.TryGetXYAround(xy0);

            foreach (var xy1 in xyAround)
            {
                CellUnitWorker.SetBotUnit(UnitTypes.Pawn, true, 150, 2, ProtectRelaxTypes.Relaxed, xy1);
                CellEnvEnt_CellEnvCom(xy1).ResetAll();

                if (!isSettedForest)
                {
                    CellEnvEnt_CellEnvCom(xy1).SetNewEnvironment(EnvironmentTypes.AdultForest, xy1);
                    isSettedForest = true;
                }
            }

            xy0[X] = 8;
            xy0[Y] = 6;
            CellBuildingWorker.SetBotBuilding(BuildingTypes.City, xy0);
            CellEnvEnt_CellEnvCom(xy0).ResetAll();

            int i = 0;
            xyAround = CellUnitWorker.TryGetXYAround(xy0);
            foreach (var xy1 in xyAround)
            {
                CellUnitWorker.SetBotUnit(UnitTypes.PawnSword, true, 150, 2, ProtectRelaxTypes.Relaxed, xy1);
                CellEnvEnt_CellEnvCom(xy1).ResetAll();

                if (i == 0)
                {
                    CellUnitWorker.ResetBotUnit(xy1);
                    CellEnvEnt_CellEnvCom(xy1).SetNewEnvironment(EnvironmentTypes.Mountain, xy1);
                }

                else if (i == 3 || i == 4 || i == 6)
                {
                    CellEnvEnt_CellEnvCom(xy1).SetNewEnvironment(EnvironmentTypes.AdultForest, xy1);
                }

                else if (i == 1)
                {
                    CellEnvEnt_CellEnvCom(xy1).SetNewEnvironment(EnvironmentTypes.Hill, xy1);
                }


                i++;
            }
        }



        GameObject CreateGameObject(GameObject go, Sprite sprite, int x, int y, GameObject mainGameGO2)
        {
            var goo = GameObject.Instantiate(go, mainGameGO2.transform.position + new Vector3(x, y, mainGameGO2.transform.position.z), mainGameGO2.transform.rotation);

            var SR = goo.GetComponent<SpriteRenderer>();
            SR.sprite = sprite;

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

    public void Dispose()
    {
        EndGameEntEndGameCom.Dispose();
    }
}
