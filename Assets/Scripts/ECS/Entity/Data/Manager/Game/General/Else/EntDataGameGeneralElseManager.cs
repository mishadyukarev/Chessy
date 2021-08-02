using Assets.Scripts;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Entities.Game.General.Else.Data.Containers;
using Assets.Scripts.ECS.Game.Components;
using Assets.Scripts.Workers;
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

public sealed class EntDataGameGeneralElseManager
{
    internal GameObject BackGroundGO;
    internal SpriteRenderer BackGroundSR;


    private MistakeEconomyEventDataContainer _mistakeEconomyEventDataContainer;


    #region Cells

    private EcsEntity _cellsInfoEnt;
    internal ref XyStartCellsComponent CellsInfoEnt_XyStartCellsCom => ref _cellsInfoEnt.Get<XyStartCellsComponent>();


    private EcsEntity _cellFireInfoEnt;
    internal ref XyAmountCellFireInGame CellFireInfoEnt_XyCellFireInfoCom => ref _cellFireInfoEnt.Get<XyAmountCellFireInGame>();

    #endregion


    #region Else


    private EcsEntity _fromInfoEnt;
    internal ref FromInfoComponent FromInfoEnt_FromInfoCom => ref _fromInfoEnt.Get<FromInfoComponent>();

    #endregion


    internal EntDataGameGeneralElseManager(EcsWorld gameWorld, EntDataCommonElseManager entCommonManager)
    {
        CreateInfoEnts(gameWorld);
        CreateSelectorEnts(gameWorld);
        new AvailableCellsContainer(gameWorld);

        BackGroundGO = GameObject.Instantiate(entCommonManager.ResourcesEnt_ResourcesCommonCom.PrefabConfig.BackGroundCollider2D,
        Instance.transform.position + new Vector3(7, 5.5f, 2), Instance.transform.rotation);

        BackGroundGO.transform.SetParent(entCommonManager.ToggleSceneParentGOZoneEnt_ParentCom.ParentGO.transform);
        BackGroundSR = BackGroundGO.GetComponent<SpriteRenderer>();
        BackGroundSR.transform.rotation = PhotonNetwork.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);



        _fromInfoEnt = gameWorld.NewEntity()
            .Replace(new FromInfoComponent());


        new MistakeEconomyEventDataWorker(gameWorld);
    }


    private void CreateSelectorEnts(EcsWorld gameWorld)
    {

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


        new InfoUnitsDataContainer(gameWorld);
        new InfoBuidlingsDataContainer(gameWorld);


        #endregion
    }
}