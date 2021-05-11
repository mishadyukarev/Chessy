using Leopotam.Ecs;
using Photon.Realtime;
using System.Collections.Generic;
using static MainGame;

internal class CellFinderWay
{
    private EcsComponentRef<CellComponent>[,] _cellComponentRef = default;
    private EcsComponentRef<CellComponent.EnvironmentComponent>[,] _cellEnvironmentComponentRef = default;
    private EcsComponentRef<CellComponent.SupportVisionComponent>[,] _cellSupportVisionComponentRef = default;
    private EcsComponentRef<CellComponent.UnitComponent>[,] _cellUnitComponentRef = default;
    private EcsComponentRef<CellComponent.BuildingComponent>[,] _cellBuildingComponentRef = default;

    private ref CellComponent CellComponent(params int[] xy)
        => ref _cellComponentRef[xy[InstanceGame.StartValuesGameConfig.X], xy[InstanceGame.StartValuesGameConfig.Y]].Unref();
    private ref CellComponent.UnitComponent CellUnitComponent(params int[] xy)
        => ref _cellUnitComponentRef[xy[InstanceGame.StartValuesGameConfig.X], xy[InstanceGame.StartValuesGameConfig.Y]].Unref();
    private ref CellComponent.EnvironmentComponent CellEnvironmentComponent(params int[] xy)
        => ref _cellEnvironmentComponentRef[xy[InstanceGame.StartValuesGameConfig.X], xy[InstanceGame.StartValuesGameConfig.Y]].Unref();
    private ref CellComponent.SupportVisionComponent CellSupportVisionComponent(params int[] xy)
        => ref _cellSupportVisionComponentRef[xy[InstanceGame.StartValuesGameConfig.X], xy[InstanceGame.StartValuesGameConfig.Y]].Unref();
    private ref CellComponent.BuildingComponent CellBuildingComponent(params int[] xy)
        => ref _cellBuildingComponentRef[xy[InstanceGame.StartValuesGameConfig.X], xy[InstanceGame.StartValuesGameConfig.Y]].Unref();
    internal void InitAfterECS(ECSmanager eCSmanager)
    {
        var entitiesGeneralManager = eCSmanager.EntitiesGeneralManager;

        _cellComponentRef = entitiesGeneralManager.CellComponentRef;
        _cellUnitComponentRef = entitiesGeneralManager.CellUnitComponentRef;
        _cellEnvironmentComponentRef = entitiesGeneralManager.CellEnvironmentComponentRef;
        _cellSupportVisionComponentRef = entitiesGeneralManager.CellSupportVisionComponentRef;
        _cellBuildingComponentRef = entitiesGeneralManager.CellBuildingComponentRef;
    }


    internal List<int[]> GetCellsForShift(int[] xyStartCell)
    {
        var listAvailable = TryGetXYAround(xyStartCell);

        var xyAvailableCellsForShift = new List<int[]>();

        foreach (var xy in listAvailable)
        {
            if (!CellEnvironmentComponent(xy).HaveMountain)
            {
                if (CellUnitComponent(xyStartCell).AmountSteps >= CellUnitComponent(xy).NeedAmountSteps(CellEnvironmentComponent(xy).ListEnvironmentTypes)
                    || CellUnitComponent(xyStartCell).HaveMaxSteps)
                {
                    xyAvailableCellsForShift.Add(xy);
                }
            }
        }
        return xyAvailableCellsForShift;
    }

    internal List<int[]> GetCellsForAttack(int[] xyStartCell, Player player)
    {
        var listAvailable = TryGetXYAround(xyStartCell);

        var xyAvailableCellsForAttack = new List<int[]>();

        foreach (var xy in listAvailable)
        {
            if (!CellEnvironmentComponent(xy).HaveMountain)
            {
                if (CellUnitComponent(xy).HaveUnit)
                {
                    if (CellUnitComponent(xyStartCell).AmountSteps >= CellUnitComponent(xyStartCell).NeedAmountSteps(CellEnvironmentComponent(xyStartCell).ListEnvironmentTypes)
                        || CellUnitComponent(xyStartCell).HaveMaxSteps)
                    {
                        if (player.ActorNumber != CellUnitComponent(xy).ActorNumber)
                        {
                            xyAvailableCellsForAttack.Add(xy);
                        }
                    }
                }
            }
        }

        return xyAvailableCellsForAttack;
    }

    internal List<int[]> TryGetXYAround(int[] xyStartCell)
    {
        var xyAvailableCells = new List<int[]>();
        var xyResultCell = new int[MainGame.InstanceGame.StartValuesGameConfig.XY_FOR_ARRAY];

        for (int i = 0; i < (int)DirectTypes.LeftDown + 1; i++)
        {
            var xyDirectCell = GetXYDirect((DirectTypes)i);

            xyResultCell[0] = xyStartCell[0] + xyDirectCell[0];
            xyResultCell[1] = xyStartCell[1] + xyDirectCell[1];

            xyAvailableCells.Add(MainGame.InstanceGame.CellManager.CellBaseOperations.CopyXY(xyResultCell));
        }

        return xyAvailableCells;
    }


    internal int[] GetXYCell(int[] xyStartCell, DirectTypes directType)
    {
        var xyResultCell = new int[MainGame.InstanceGame.StartValuesGameConfig.XY_FOR_ARRAY];

        var xyDirectCell = GetXYDirect(directType);

        xyResultCell[0] = xyStartCell[0] + xyDirectCell[0];
        xyResultCell[1] = xyStartCell[1] + xyDirectCell[1];

        return xyResultCell;
    }

    internal int[] GetXYDirect(DirectTypes direct)
    {
        var xyDirectCell = new int[MainGame.InstanceGame.StartValuesGameConfig.XY_FOR_ARRAY];

        switch (direct)
        {
            case DirectTypes.Right:
                xyDirectCell[0] = 1;
                xyDirectCell[1] = 0;
                break;

            case DirectTypes.Left:
                xyDirectCell[0] = -1;
                xyDirectCell[1] = 0;
                break;

            case DirectTypes.Up:
                xyDirectCell[0] = 0;
                xyDirectCell[1] = 1;
                break;

            case DirectTypes.Down:
                xyDirectCell[0] = 0;
                xyDirectCell[1] = -1;
                break;

            case DirectTypes.RightUp:
                xyDirectCell[0] = 1;
                xyDirectCell[1] = 1;
                break;

            case DirectTypes.LeftUp:
                xyDirectCell[0] = -1;
                xyDirectCell[1] = 1;
                break;

            case DirectTypes.RightDown:
                xyDirectCell[0] = 1;
                xyDirectCell[1] = -1;
                break;

            case DirectTypes.LeftDown:
                xyDirectCell[0] = -1;
                xyDirectCell[1] = -1;
                break;

            default:
                break;
        }

        return xyDirectCell;
    }
}