using Assets.Scripts;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.Components;
using Assets.Scripts.ECS.Game.General.Entities;
using Assets.Scripts.ECS.Game.General.Entities.Containers;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Game.Else;
using Assets.Scripts.Workers.Game.Else.Cell;
using Assets.Scripts.Workers.Game.Else.CellBuildings;
using Assets.Scripts.Workers.Game.Else.CellEnvir;
using Assets.Scripts.Workers.Game.Else.Fire;
using Assets.Scripts.Workers.Game.Else.Units;
using Assets.Scripts.Workers.Info;
using ExitGames.Client.Photon.StructWrapping;
using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;
using static Assets.Scripts.CellEnvirDataWorker;
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
    private CellEnvirEntsContainer _cellEnvirEntsContainer;
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
    internal ref UnitsInStandartConditionComponent KingInfoEnt_AmountUnitsInRelaxCom => ref _kingInfoEnt.Get<UnitsInStandartConditionComponent>();


    private EcsEntity _pawnInfoEnt;
    internal ref AmountUnitsInGameComponent PawnInfoEnt_AmountUnitsInGameCom => ref _pawnInfoEnt.Get<AmountUnitsInGameComponent>();
    internal ref AmountUnitsInInventorDictComponent PawnInfoEnt_AmountUnitsInInventorDictCom => ref _pawnInfoEnt.Get<AmountUnitsInInventorDictComponent>();
    internal ref UnitsInStandartConditionComponent PawnInfoEnt_AmountUnitsInRelaxCom => ref _pawnInfoEnt.Get<UnitsInStandartConditionComponent>();



    private EcsEntity _pawnSwordInfoEnt;
    internal ref AmountUnitsInGameComponent PawnSwordInfoEnt_AmountUnitsInGameCom => ref _pawnSwordInfoEnt.Get<AmountUnitsInGameComponent>();
    internal ref AmountUnitsInInventorDictComponent PawnSwordInfoEnt_AmountUnitsInInventorDictCom => ref _pawnSwordInfoEnt.Get<AmountUnitsInInventorDictComponent>();
    internal ref UnitsInStandartConditionComponent PawnSwordInfoEnt_AmountUnitsInRelaxCom => ref _pawnSwordInfoEnt.Get<UnitsInStandartConditionComponent>();


    private EcsEntity _rookInfoEnt;
    internal ref AmountUnitsInGameComponent RookInfoEnt_AmountUnitsInGameCom => ref _rookInfoEnt.Get<AmountUnitsInGameComponent>();
    internal ref AmountUnitsInInventorDictComponent RookInfoEnt_AmountUnitsInInventorDictCom => ref _rookInfoEnt.Get<AmountUnitsInInventorDictComponent>();
    internal ref UnitsInStandartConditionComponent RookInfoEnt_AmountUnitsInRelaxCom => ref _rookInfoEnt.Get<UnitsInStandartConditionComponent>();


    private EcsEntity _rookCrossbowInfoEnt;
    internal ref AmountUnitsInGameComponent RookCrossbowInfoEnt_AmountUnitsInGameCom => ref _rookCrossbowInfoEnt.Get<AmountUnitsInGameComponent>();
    internal ref AmountUnitsInInventorDictComponent RookCrossbowInfoEnt_AmountUnitsInInventorDictCom => ref _rookCrossbowInfoEnt.Get<AmountUnitsInInventorDictComponent>();
    internal ref UnitsInStandartConditionComponent RookCrossbowInfoEnt_AmountUnitsInRelaxCom => ref _rookCrossbowInfoEnt.Get<UnitsInStandartConditionComponent>();


    private EcsEntity _bishopInfoInGameEnt;
    internal ref AmountUnitsInGameComponent BishopInfoEnt_AmountUnitsInGameCom => ref _bishopInfoInGameEnt.Get<AmountUnitsInGameComponent>();
    internal ref AmountUnitsInInventorDictComponent BishopInfoEnt_AmountUnitsInInventorDictCom => ref _bishopInfoInGameEnt.Get<AmountUnitsInInventorDictComponent>();
    internal ref UnitsInStandartConditionComponent BishopInfoEnt_AmountUnitsInRelaxCom => ref _bishopInfoInGameEnt.Get<UnitsInStandartConditionComponent>();


    private EcsEntity _bishopCrossbowInfoEnt;
    internal ref AmountUnitsInGameComponent BishopCrossbowInfoEnt_AmountUnitsInGameCom => ref _bishopCrossbowInfoEnt.Get<AmountUnitsInGameComponent>();
    internal ref AmountUnitsInInventorDictComponent BishopCrossbowInfoEnt_AmountUnitsInInventorDictCom => ref _bishopCrossbowInfoEnt.Get<AmountUnitsInInventorDictComponent>();
    internal ref UnitsInStandartConditionComponent BishopCrossbowInfoEnt_AmountUnitsInStandartCondition => ref _bishopCrossbowInfoEnt.Get<UnitsInStandartConditionComponent>();

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

    internal EntitiesGameGeneralManager(EcsWorld gameWorld)
    {

        SpawnAndCreateCellsEnts(gameWorld);
        CreateInfoEnts(gameWorld);
        CreateSelectorEnts(gameWorld);
        CreateSound(gameWorld);

        BackGroundGO = GameObject.Instantiate(Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.PrefabConfig.BackGroundCollider2D,
        Instance.transform.position + new Vector3(7, 5.5f, 2), Instance.transform.rotation);

        Instance.ECSmanager.EntitiesCommonManager.ToggleSceneParentGOZoneEnt_ParentGOZoneCom.AttachToCurrentParent(BackGroundGO.transform);
        BackGroundSR = BackGroundGO.GetComponent<SpriteRenderer>();
        BackGroundSR.transform.rotation = Instance.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);



        _inputEnt = gameWorld.NewEntity()
            .Replace(new InputComponent());



        _fromInfoEnt = gameWorld.NewEntity()
            .Replace(new FromInfoComponent());
    }


    private void SpawnAndCreateCellsEnts(EcsWorld gameWorld)
    {
        var cellGO = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.PrefabConfig.CellGO;
        var whiteCellSR = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SpritesConfig.WhiteSprite;
        var blackCellSR = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SpritesConfig.BlackSprite;

        var cellGOs = new GameObject[CELL_COUNT_X, CELL_COUNT_Y];

        var supportParentForCells = new GameObject("Cells");
        Instance.ECSmanager.EntitiesCommonManager.ToggleSceneParentGOZoneEnt_ParentGOZoneCom.AttachToCurrentParent(supportParentForCells.transform);


        for (int x = 0; x < CELL_COUNT_X; x++)
            for (int y = 0; y < CELL_COUNT_Y; y++)
            {
                if (y % 2 == 0)
                {
                    if (x % 2 == 0)
                    {
                        cellGOs[x, y] = CreateGameObject(cellGO, blackCellSR, x, y, Instance.gameObject);
                        SetActive(cellGOs[x, y], x, y);
                    }
                    if (x % 2 != 0)
                    {
                        cellGOs[x, y] = CreateGameObject(cellGO, whiteCellSR, x, y, Instance.gameObject);
                        SetActive(cellGOs[x, y], x, y);
                    }
                }
                if (y % 2 != 0)
                {
                    if (x % 2 != 0)
                    {
                        cellGOs[x, y] = CreateGameObject(cellGO, blackCellSR, x, y, Instance.gameObject);
                        SetActive(cellGOs[x, y], x, y);
                    }
                    if (x % 2 == 0)
                    {
                        cellGOs[x, y] = CreateGameObject(cellGO, whiteCellSR, x, y, Instance.gameObject);
                        SetActive(cellGOs[x, y], x, y);
                    }
                }

                GameObject CreateGameObject(GameObject cellGOForCreation, Sprite sprite, int xxx, int yyy, GameObject mainGameGO)
                {
                    var go = GameObject.Instantiate(cellGOForCreation, mainGameGO.transform.position + new Vector3(xxx, yyy, mainGameGO.transform.position.z), mainGameGO.transform.rotation);

                    go.name = "Cell";

                    go.transform.Find("Cell").GetComponent<SpriteRenderer>().sprite = sprite;

                    return go;
                }

                void SetActive(GameObject go, int xx, int yy)
                {
                    if (xx >= 0 && yy == 0 || xx >= 0 && yy == 10 ||
                        xx == 1 && yy >= 0 || xx == 13 && yy >= 0 ||
                    xx == 0 && yy >= 0 || xx == 14 && yy >= 0 ||
                    xx == 1 && yy == 1 || xx == 2 && yy == 1 || xx == 12 && yy == 1 || xx == 13 && yy == 1 ||
                    xx == 1 && yy == 9 || xx == 2 && yy == 9 || xx == 12 && yy == 9 || xx == 13 && yy == 9)
                        go.SetActive(false);
                }

                cellGOs[x, y].transform.SetParent(supportParentForCells.transform);

                cellGOs[x, y].transform.rotation = Instance.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);
            }

        _cellSupVisBlocksContainer = new CellSupVisBlocksEntsContainer(cellGOs, gameWorld);
        new CellSupVisBlocksWorker(_cellSupVisBlocksContainer);

        _cellSupVisBarsContainer = new CellSupVisBarsEntsContainer(cellGOs, gameWorld);
        new CellSupVisBarsWorker(_cellSupVisBarsContainer);


        _cellSupVisEntsContainer = new CellSupVisEntsContainer(cellGOs, gameWorld);
        new CellSupVisWorker(_cellSupVisEntsContainer);

        _cellFireEntsContainer = new CellFireEntsContainer(cellGOs, gameWorld);
        new CellFireDataWorker(_cellFireEntsContainer);
        new CellFireVisWorker(_cellFireEntsContainer);

        _cellUnitEntsContainer = new CellUnitEntsContainer(cellGOs, gameWorld);
        new CellUnitsDataWorker(_cellUnitEntsContainer);
        new CellUnitsVisWorker(_cellUnitEntsContainer);

        _cellBuildingEntsContainer = new CellBuildingEntsContainer(cellGOs, gameWorld);
        new CellBuildingsDataWorker(_cellBuildingEntsContainer);
        new CellBuildingsVisWorker(_cellBuildingEntsContainer);

        _cellEnvirEntsContainer = new CellEnvirEntsContainer(cellGOs, gameWorld);
        new CellEnvirDataWorker(_cellEnvirEntsContainer);
        new CellEnvirVisWorker(_cellEnvirEntsContainer);

        _cellContainer = new CellEntsContainer(cellGOs, gameWorld);
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
            // Bot
        }
    }

    private void CreateSelectorEnts(EcsWorld gameWorld)
    {
        _selectorEnt = gameWorld.NewEntity()
            .Replace(new SelectorComponent())
            .Replace(new RaycastHit2DComponent())
            .Replace(new UnitTypeComponent())
            .Replace(new UpgradeModTypeComponent());



        _xyCurrentCellEnt = gameWorld.NewEntity()
            .Replace(new XyCellComponent(new int[2]));

        _xySelectedCellEnt = gameWorld.NewEntity()
            .Replace(new XyCellComponent(new int[2]));

        _xyPreviousCellEnt = gameWorld.NewEntity()
            .Replace(new XyCellComponent(new int[2]));

        _xyPreviousVisionCellEnt = gameWorld.NewEntity()
            .Replace(new XyCellComponent(new int[2]));

        _availableCellEntsContainer = new AvailableCellEntsContainer(gameWorld);
        new AvailableCellsEntsWorker(_availableCellEntsContainer);
    }

    private void CreateInfoEnts(EcsWorld gameWorld)
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
            .Replace(new AmountUnitsInGameComponent(new Dictionary<bool, List<int[]>>()))
            .Replace(new AmountUnitsInInventorDictComponent(new Dictionary<bool, int>()))
            .Replace(new UnitsInStandartConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));

        _pawnInfoEnt = gameWorld.NewEntity()
            .Replace(new AmountUnitsInGameComponent(new Dictionary<bool, List<int[]>>()))
            .Replace(new AmountUnitsInInventorDictComponent(new Dictionary<bool, int>()))
            .Replace(new UnitsInStandartConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));

        _pawnSwordInfoEnt = gameWorld.NewEntity()
            .Replace(new AmountUnitsInGameComponent(new Dictionary<bool, List<int[]>>()))
            .Replace(new AmountUnitsInInventorDictComponent(new Dictionary<bool, int>()))
            .Replace(new UnitsInStandartConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));

        _rookInfoEnt = gameWorld.NewEntity()
            .Replace(new AmountUnitsInGameComponent(new Dictionary<bool, List<int[]>>()))
            .Replace(new AmountUnitsInInventorDictComponent(new Dictionary<bool, int>()))
            .Replace(new UnitsInStandartConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));

        _rookCrossbowInfoEnt = gameWorld.NewEntity()
            .Replace(new AmountUnitsInGameComponent(new Dictionary<bool, List<int[]>>()))
            .Replace(new AmountUnitsInInventorDictComponent(new Dictionary<bool, int>()))
            .Replace(new UnitsInStandartConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));

        _bishopInfoInGameEnt = gameWorld.NewEntity()
            .Replace(new AmountUnitsInGameComponent(new Dictionary<bool, List<int[]>>()))
            .Replace(new AmountUnitsInInventorDictComponent(new Dictionary<bool, int>()))
            .Replace(new UnitsInStandartConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));

        _bishopCrossbowInfoEnt = gameWorld.NewEntity()
            .Replace(new AmountUnitsInGameComponent(new Dictionary<bool, List<int[]>>()))
            .Replace(new AmountUnitsInInventorDictComponent(new Dictionary<bool, int>()))
            .Replace(new UnitsInStandartConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));



        _cityInfoEnt = gameWorld.NewEntity()
            .Replace(new BuildingsInGameDictComponent(new Dictionary<bool, List<int[]>>()));

        _farmsInfoEnt = gameWorld.NewEntity()
            .Replace(new BuildingsInGameDictComponent(new Dictionary<bool, List<int[]>>()))
            .Replace(new AmountUpgradesDictComponent(new Dictionary<bool, int>()));

        _woodcuttersInfoEnt = gameWorld.NewEntity()
            .Replace(new BuildingsInGameDictComponent(new Dictionary<bool, List<int[]>>()))
            .Replace(new AmountUpgradesDictComponent(new Dictionary<bool, int>()));

        _minesInfoEnt = gameWorld.NewEntity()
            .Replace(new BuildingsInGameDictComponent(new Dictionary<bool, List<int[]>>()))
            .Replace(new AmountUpgradesDictComponent(new Dictionary<bool, int>()));



        _foodInfoEnt = gameWorld.NewEntity()
            .Replace(new AmountResourcesDictComponent(new Dictionary<bool, int>()));

        _woodInfoEnt = gameWorld.NewEntity()
            .Replace(new AmountResourcesDictComponent(new Dictionary<bool, int>()));

        _oreInfoEnt = gameWorld.NewEntity()
            .Replace(new AmountResourcesDictComponent(new Dictionary<bool, int>()));

        _ironInfoEnt = gameWorld.NewEntity()
            .Replace(new AmountResourcesDictComponent(new Dictionary<bool, int>()));

        _goldInfoEnt = gameWorld.NewEntity()
            .Replace(new AmountResourcesDictComponent(new Dictionary<bool, int>()));
    }

    private void CreateSound(EcsWorld gameWorld)
    {
        var audioSourceParentGO = new GameObject("AudioSource");
        Instance.ECSmanager.EntitiesCommonManager.ToggleSceneParentGOZoneEnt_ParentGOZoneCom.AttachToCurrentParent(audioSourceParentGO.transform);

        MistakeAudioSource = audioSourceParentGO.AddComponent<AudioSource>();
        MistakeAudioSource.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.MistakeAudioClip;

        AttackAudioSource = audioSourceParentGO.AddComponent<AudioSource>();
        AttackAudioSource.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.AttackSwordAudioClip;


        var attackAS = audioSourceParentGO.AddComponent<AudioSource>();
        attackAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.AttackArcherAC;
        attackAS.volume = 0.6f;
        _attackArcherSoundEnt = gameWorld.NewEntity()
            .Replace(new AudioSourceComponent(attackAS));


        var pickArcherAS = audioSourceParentGO.AddComponent<AudioSource>();
        pickArcherAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.PickArcherAudioClip;
        pickArcherAS.volume = 0.7f;
        _pickArcherSoundEnt = gameWorld.NewEntity()
            .Replace(new AudioSourceComponent(pickArcherAS));


        var pickMeleeAS = audioSourceParentGO.AddComponent<AudioSource>();
        pickMeleeAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.PickMeleeAC;
        pickMeleeAS.volume = 0.1f;
        _pickMeleeSoundEnt = gameWorld.NewEntity()
            .Replace(new AudioSourceComponent(pickMeleeAS));


        var buildingAS = audioSourceParentGO.AddComponent<AudioSource>();
        buildingAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.BuildingAC;
        buildingAS.volume = 0.1f;
        _buildingSoundEnt = gameWorld.NewEntity()
            .Replace(new AudioSourceComponent(buildingAS));


        var settingAS = audioSourceParentGO.AddComponent<AudioSource>();
        settingAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.SettingUnitAC;
        _settingSoundEnt = gameWorld.NewEntity()
            .Replace(new AudioSourceComponent(settingAS));


        var fireAS = audioSourceParentGO.AddComponent<AudioSource>();
        fireAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.FireAC;
        fireAS.volume = 0.2f;
        _fireSoundEnt = gameWorld.NewEntity()
            .Replace(new AudioSourceComponent(fireAS));


        var buyAS = audioSourceParentGO.AddComponent<AudioSource>();
        buyAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.BuyAC;
        buyAS.volume = 0.3f;
        _buySoundEnt = gameWorld.NewEntity()
            .Replace(new AudioSourceComponent(buyAS));


        var meltingAS = audioSourceParentGO.AddComponent<AudioSource>();
        meltingAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.Melting_Clip;
        meltingAS.volume = 0.3f;
        _meltingSoundEnt = gameWorld.NewEntity()
            .Replace(new AudioSourceComponent(meltingAS));



        var destroyAS = audioSourceParentGO.AddComponent<AudioSource>();
        destroyAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.Destroy_Clip;
        destroyAS.volume = 0.3f;
        _destroySoundEnt = gameWorld.NewEntity()
            .Replace(new AudioSourceComponent(destroyAS));


        var upgradeUnitMeleeAS = audioSourceParentGO.AddComponent<AudioSource>();
        upgradeUnitMeleeAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.UpgradeUnitMelee_Clip;
        upgradeUnitMeleeAS.volume = 0.2f;
        _upgradeUnitMeleeSoundEnt = gameWorld.NewEntity()
            .Replace(new AudioSourceComponent(upgradeUnitMeleeAS));


        var seedingAS = audioSourceParentGO.AddComponent<AudioSource>();
        seedingAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.Seeding_Clip;
        seedingAS.volume = 0.2f;
        _seedingSoundEnt = gameWorld.NewEntity()
            .Replace(new AudioSourceComponent(seedingAS));


        var shiftUnitAS = audioSourceParentGO.AddComponent<AudioSource>();
        shiftUnitAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.ShiftUnit_Clip;
        shiftUnitAS.volume = 0.6f;
        _shiftUnitSoundEnt = gameWorld.NewEntity()
            .Replace(new AudioSourceComponent(shiftUnitAS));


        var truceAS = audioSourceParentGO.AddComponent<AudioSource>();
        truceAS.clip = Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SoundConfig.Truce_Clip;
        truceAS.volume = 0.6f;
        _truceSoundEnt = gameWorld.NewEntity()
            .Replace(new AudioSourceComponent(truceAS));
    }
}