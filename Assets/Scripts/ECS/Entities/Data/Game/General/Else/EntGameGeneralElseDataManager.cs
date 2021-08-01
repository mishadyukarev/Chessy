using Assets.Scripts;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Entities.Game.General.Else.Data.Containers;
using Assets.Scripts.ECS.Game.Components;
using Assets.Scripts.Workers.Game.Else;
using Assets.Scripts.Workers.Game.Else.Data;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using ExitGames.Client.Photon.StructWrapping;
using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;
using static Assets.Scripts.Main;

public sealed class EntGameGeneralElseDataManager
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

    #endregion


    #region Units



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


    internal EntGameGeneralElseDataManager(EcsWorld gameWorld, EntCommonManager entCommonManager)
    {
        CreateInfoEnts(gameWorld);
        CreateSelectorEnts(gameWorld);
        new AvailableCellsContainer(gameWorld);

        BackGroundGO = GameObject.Instantiate(entCommonManager.ResourcesEnt_ResourcesCommonCom.PrefabConfig.BackGroundCollider2D,
        Instance.transform.position + new Vector3(7, 5.5f, 2), Instance.transform.rotation);

        BackGroundGO.transform.SetParent(entCommonManager.ToggleSceneParentGOZoneEnt_ParentCom.ParentGO.transform);
        BackGroundSR = BackGroundGO.GetComponent<SpriteRenderer>();
        BackGroundSR.transform.rotation = PhotonNetwork.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);


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


        new InfoUnitsContainer(gameWorld);



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