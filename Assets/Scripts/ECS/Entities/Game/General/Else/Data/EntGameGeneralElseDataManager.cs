using Assets.Scripts;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Entities.Game.General.Else.Data.Containers;
using Assets.Scripts.ECS.Game.Components;
using Assets.Scripts.ECS.Game.General.Entities.Containers;
using Assets.Scripts.Workers.Game.Else;
using Assets.Scripts.Workers.Game.Else.Data;
using ExitGames.Client.Photon.StructWrapping;
using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;
using static Assets.Scripts.Main;

public sealed class EntGameGeneralElseDataManager : EntitiesManager
{
    internal GameObject BackGroundGO;
    internal SpriteRenderer BackGroundSR;


    private MistakeEconomyEventDataContainer _mistakeEconomyEventDataContainer;


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


    private AvailableCellsDataContainerEnts _availableCellEntsContainer;

    #endregion


    #region Units

    private EcsEntity _kingInfoEnt;
    internal ref XyUnitsInGameDictComponent KingInfoEnt_AmountUnitsInGameCom => ref _kingInfoEnt.Get<XyUnitsInGameDictComponent>();
    internal ref AmountUnitsInInventorDictComponent KingInfoEnt_AmountUnitsInInventorDictCom => ref _kingInfoEnt.Get<AmountUnitsInInventorDictComponent>();
    internal ref XyUnitsInConditionComponent KingInfoEnt_AmountUnitsInRelaxCom => ref _kingInfoEnt.Get<XyUnitsInConditionComponent>();


    private EcsEntity _pawnInfoEnt;
    internal ref XyUnitsInGameDictComponent PawnInfoEnt_AmountUnitsInGameCom => ref _pawnInfoEnt.Get<XyUnitsInGameDictComponent>();
    internal ref AmountUnitsInInventorDictComponent PawnInfoEnt_AmountUnitsInInventorDictCom => ref _pawnInfoEnt.Get<AmountUnitsInInventorDictComponent>();
    internal ref XyUnitsInConditionComponent PawnInfoEnt_AmountUnitsInRelaxCom => ref _pawnInfoEnt.Get<XyUnitsInConditionComponent>();



    private EcsEntity _pawnSwordInfoEnt;
    internal ref XyUnitsInGameDictComponent PawnSwordInfoEnt_AmountUnitsInGameCom => ref _pawnSwordInfoEnt.Get<XyUnitsInGameDictComponent>();
    internal ref AmountUnitsInInventorDictComponent PawnSwordInfoEnt_AmountUnitsInInventorDictCom => ref _pawnSwordInfoEnt.Get<AmountUnitsInInventorDictComponent>();
    internal ref XyUnitsInConditionComponent PawnSwordInfoEnt_AmountUnitsInRelaxCom => ref _pawnSwordInfoEnt.Get<XyUnitsInConditionComponent>();


    private EcsEntity _rookInfoEnt;
    internal ref XyUnitsInGameDictComponent RookInfoEnt_AmountUnitsInGameCom => ref _rookInfoEnt.Get<XyUnitsInGameDictComponent>();
    internal ref AmountUnitsInInventorDictComponent RookInfoEnt_AmountUnitsInInventorDictCom => ref _rookInfoEnt.Get<AmountUnitsInInventorDictComponent>();
    internal ref XyUnitsInConditionComponent RookInfoEnt_AmountUnitsInRelaxCom => ref _rookInfoEnt.Get<XyUnitsInConditionComponent>();


    private EcsEntity _rookCrossbowInfoEnt;
    internal ref XyUnitsInGameDictComponent RookCrossbowInfoEnt_AmountUnitsInGameCom => ref _rookCrossbowInfoEnt.Get<XyUnitsInGameDictComponent>();
    internal ref AmountUnitsInInventorDictComponent RookCrossbowInfoEnt_AmountUnitsInInventorDictCom => ref _rookCrossbowInfoEnt.Get<AmountUnitsInInventorDictComponent>();
    internal ref XyUnitsInConditionComponent RookCrossbowInfoEnt_AmountUnitsInRelaxCom => ref _rookCrossbowInfoEnt.Get<XyUnitsInConditionComponent>();


    private EcsEntity _bishopInfoInGameEnt;
    internal ref XyUnitsInGameDictComponent BishopInfoEnt_AmountUnitsInGameCom => ref _bishopInfoInGameEnt.Get<XyUnitsInGameDictComponent>();
    internal ref AmountUnitsInInventorDictComponent BishopInfoEnt_AmountUnitsInInventorDictCom => ref _bishopInfoInGameEnt.Get<AmountUnitsInInventorDictComponent>();
    internal ref XyUnitsInConditionComponent BishopInfoEnt_AmountUnitsInRelaxCom => ref _bishopInfoInGameEnt.Get<XyUnitsInConditionComponent>();


    private EcsEntity _bishopCrossbowInfoEnt;
    internal ref XyUnitsInGameDictComponent BishopCrossbowInfoEnt_AmountUnitsInGameCom => ref _bishopCrossbowInfoEnt.Get<XyUnitsInGameDictComponent>();
    internal ref AmountUnitsInInventorDictComponent BishopCrossbowInfoEnt_AmountUnitsInInventorDictCom => ref _bishopCrossbowInfoEnt.Get<AmountUnitsInInventorDictComponent>();
    internal ref XyUnitsInConditionComponent BishopCrossbowInfoEnt_AmountUnitsInStandartCondition => ref _bishopCrossbowInfoEnt.Get<XyUnitsInConditionComponent>();

    #endregion


    #region Cells

    private EcsEntity _cellsInfoEnt;
    internal ref XyStartCellsComponent CellsInfoEnt_XyStartCellsCom => ref _cellsInfoEnt.Get<XyStartCellsComponent>();


    #region CellBuildings

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


    #region Fire

    private EcsEntity _cellFireInfoEnt;
    internal ref XyAmountCellFireInGame CellFireInfoEnt_XyCellFireInfoCom => ref _cellFireInfoEnt.Get<XyAmountCellFireInGame>();

    #endregion

    #endregion


    #region Else

    private EcsEntity _inputEnt;
    internal ref InputComponent InputEnt_InputCom => ref _inputEnt.Get<InputComponent>();


    private EcsEntity _fromInfoEnt;
    internal ref FromInfoComponent FromInfoEnt_FromInfoCom => ref _fromInfoEnt.Get<FromInfoComponent>();

    #endregion


    internal EntGameGeneralElseDataManager(EcsWorld gameWorld)
    {
        CreateInfoEnts(gameWorld);
        CreateSelectorEnts(gameWorld);

        BackGroundGO = GameObject.Instantiate(Instance.ECSmanager.EntCommonManager.ResourcesEnt_ResourcesCommonCom.PrefabConfig.BackGroundCollider2D,
        Instance.transform.position + new Vector3(7, 5.5f, 2), Instance.transform.rotation);

        BackGroundGO.transform.SetParent(Instance.ECSmanager.EntCommonManager.ToggleSceneParentGOZoneEnt_ParentCom.ParentGO.transform);
        BackGroundSR = BackGroundGO.GetComponent<SpriteRenderer>();
        BackGroundSR.transform.rotation = Instance.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);


        _inputEnt = gameWorld.NewEntity()
            .Replace(new InputComponent());


        _fromInfoEnt = gameWorld.NewEntity()
            .Replace(new FromInfoComponent());



        _mistakeEconomyEventDataContainer = new MistakeEconomyEventDataContainer(gameWorld);
        new MistakeEconomyEventDataWorker(_mistakeEconomyEventDataContainer);
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

        _availableCellEntsContainer = new AvailableCellsDataContainerEnts(gameWorld);
        new AvailableCellsEntsWorker(_availableCellEntsContainer);
    }

    private void CreateInfoEnts(EcsWorld gameWorld)
    {
        #region Cell

        var listMaster = new List<int[]>();
        var listOther = new List<int[]>();

        for (int x = 0; x < CELL_COUNT_X; x++)
            for (int y = 0; y < CELL_COUNT_Y; y++)
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


        _cellFireInfoEnt = gameWorld.NewEntity()
            .Replace(new XyAmountCellFireInGame(new List<int[]>()));


        _kingInfoEnt = gameWorld.NewEntity()
            .Replace(new XyUnitsInGameDictComponent(new Dictionary<bool, List<int[]>>()))
            .Replace(new AmountUnitsInInventorDictComponent(new Dictionary<bool, int>()))
            .Replace(new XyUnitsInConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));

        _pawnInfoEnt = gameWorld.NewEntity()
            .Replace(new XyUnitsInGameDictComponent(new Dictionary<bool, List<int[]>>()))
            .Replace(new AmountUnitsInInventorDictComponent(new Dictionary<bool, int>()))
            .Replace(new XyUnitsInConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));

        _pawnSwordInfoEnt = gameWorld.NewEntity()
            .Replace(new XyUnitsInGameDictComponent(new Dictionary<bool, List<int[]>>()))
            .Replace(new AmountUnitsInInventorDictComponent(new Dictionary<bool, int>()))
            .Replace(new XyUnitsInConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));

        _rookInfoEnt = gameWorld.NewEntity()
            .Replace(new XyUnitsInGameDictComponent(new Dictionary<bool, List<int[]>>()))
            .Replace(new AmountUnitsInInventorDictComponent(new Dictionary<bool, int>()))
            .Replace(new XyUnitsInConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));

        _rookCrossbowInfoEnt = gameWorld.NewEntity()
            .Replace(new XyUnitsInGameDictComponent(new Dictionary<bool, List<int[]>>()))
            .Replace(new AmountUnitsInInventorDictComponent(new Dictionary<bool, int>()))
            .Replace(new XyUnitsInConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));

        _bishopInfoInGameEnt = gameWorld.NewEntity()
            .Replace(new XyUnitsInGameDictComponent(new Dictionary<bool, List<int[]>>()))
            .Replace(new AmountUnitsInInventorDictComponent(new Dictionary<bool, int>()))
            .Replace(new XyUnitsInConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));

        _bishopCrossbowInfoEnt = gameWorld.NewEntity()
            .Replace(new XyUnitsInGameDictComponent(new Dictionary<bool, List<int[]>>()))
            .Replace(new AmountUnitsInInventorDictComponent(new Dictionary<bool, int>()))
            .Replace(new XyUnitsInConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));



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

        #endregion
    }
}