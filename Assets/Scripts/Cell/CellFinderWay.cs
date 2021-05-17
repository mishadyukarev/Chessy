using Leopotam.Ecs;
using Photon.Realtime;
using System.Collections.Generic;

internal class CellFinderWay
{
    private EntitiesGeneralManager _eGM;
    private CellBaseOperations _cellBaseOperations;
    private StartValuesGameConfig _startValuesGameConfig;

    private int XYForArray => _startValuesGameConfig.XY_FOR_ARRAY;


    internal CellFinderWay(StartValuesGameConfig startValuesGameConfig, CellBaseOperations cellBaseOperations)
    {
        _startValuesGameConfig = startValuesGameConfig;
        _cellBaseOperations = cellBaseOperations;
    }


    internal void InitAfterECS(ECSmanager eCSmanager)
    {
        _eGM = eCSmanager.EntitiesGeneralManager;
    }


    internal List<int[]> GetCellsForShift(int[] xyStartCell)
    {
        var listAvailable = TryGetXYAround(xyStartCell);

        var xyAvailableCellsForShift = new List<int[]>();

        foreach (var xy in listAvailable)
        {
            if (!_eGM.CellEnvironmentComponent(xy).HaveMountain && !_eGM.CellUnitComponent(xy).HaveUnit)
            {
                if (_eGM.CellUnitComponent(xyStartCell).AmountSteps >= _eGM.CellUnitComponent(xy).NeedAmountSteps(_eGM.CellEnvironmentComponent(xy).ListEnvironmentTypes)
                    || _eGM.CellUnitComponent(xyStartCell).HaveMaxSteps)
                {
                    xyAvailableCellsForShift.Add(xy);
                }
            }
        }
        return xyAvailableCellsForShift;
    }

    internal void GetCellsForAttack(int[] xyStartCell, Player player, out List<int[]> availableCellsSimpleAttack, out List<int[]> availableCellsUniqueAttack)
    {
        availableCellsSimpleAttack = new List<int[]>();
        availableCellsUniqueAttack = new List<int[]>();

        for (DirectTypes directType1 = default; directType1 <= DirectTypes.LeftDown; directType1++)
        {
            var xy1 = GetXYCell(xyStartCell, directType1);

            if (!_eGM.CellEnvironmentComponent(xy1).HaveMountain && _eGM.CellUnitComponent(xy1).HaveUnit && !_eGM.CellUnitComponent(xy1).IsHisUnit(player))
            {
                switch (_eGM.CellUnitComponent(xyStartCell).UnitType)
                {
                    case UnitTypes.None:
                        break;


                    case UnitTypes.King:
                        availableCellsSimpleAttack.Add(xy1);
                        break;


                    case UnitTypes.Pawn:

                        if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                        {
                            availableCellsSimpleAttack.Add(xy1);
                        }
                        else availableCellsUniqueAttack.Add(xy1);

                        break;


                    case UnitTypes.Rook:
                        if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                        {
                            availableCellsUniqueAttack.Add(xy1);
                        }
                        else availableCellsSimpleAttack.Add(xy1);
                        break;


                    case UnitTypes.Bishop:
                        if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                        {
                            availableCellsSimpleAttack.Add(xy1);
                        }
                        else availableCellsUniqueAttack.Add(xy1);
                        break;


                    default:
                        availableCellsSimpleAttack.Add(xy1);
                        break;

                }
            }


            switch (_eGM.CellUnitComponent(xyStartCell).UnitType)
            {
                case UnitTypes.Rook:

                    if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                    {
                        var xy2 = GetXYCell(xy1, directType1);
                        if (!_eGM.CellEnvironmentComponent(xy2).HaveMountain && _eGM.CellUnitComponent(xy2).HaveUnit && !_eGM.CellUnitComponent(xy2).IsHisUnit(player))
                        {
                            availableCellsUniqueAttack.Add(xy2);
                        }
                    }

                    if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                    {
                        var xy2 = GetXYCell(xy1, directType1);
                        if (!_eGM.CellEnvironmentComponent(xy2).HaveMountain && _eGM.CellUnitComponent(xy2).HaveUnit && !_eGM.CellUnitComponent(xy2).IsHisUnit(player))
                        {
                            availableCellsSimpleAttack.Add(xy2);
                        }
                    }

                    break;


                case UnitTypes.Bishop:

                    if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                    {
                        var xy2 = GetXYCell(xy1, directType1);
                        if (!_eGM.CellEnvironmentComponent(xy2).HaveMountain && _eGM.CellUnitComponent(xy2).HaveUnit && !_eGM.CellUnitComponent(xy2).IsHisUnit(player))
                        {
                            availableCellsUniqueAttack.Add(xy2);
                        }
                    }

                    if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                    {
                        var xy2 = GetXYCell(xy1, directType1);
                        if (!_eGM.CellEnvironmentComponent(xy2).HaveMountain && _eGM.CellUnitComponent(xy2).HaveUnit && !_eGM.CellUnitComponent(xy2).IsHisUnit(player))
                        {
                            availableCellsSimpleAttack.Add(xy2);
                        }
                    }

                    break;

                default:
                    break;
            }
        }
    }

    internal List<int[]> TryGetXYAround(int[] xyStartCell)
    {
        var xyAvailableCells = new List<int[]>();
        var xyResultCell = new int[XYForArray];

        for (int i = 0; i < (int)DirectTypes.LeftDown + 1; i++)
        {
            var xyDirectCell = GetXYDirect((DirectTypes)i);

            xyResultCell[0] = xyStartCell[0] + xyDirectCell[0];
            xyResultCell[1] = xyStartCell[1] + xyDirectCell[1];

            xyAvailableCells.Add(_cellBaseOperations.CopyXY(xyResultCell));
        }

        return xyAvailableCells;
    }


    internal int[] GetXYCell(int[] xyStartCell, DirectTypes directType)
    {
        var xyResultCell = new int[XYForArray];

        var xyDirectCell = GetXYDirect(directType);

        xyResultCell[0] = xyStartCell[0] + xyDirectCell[0];
        xyResultCell[1] = xyStartCell[1] + xyDirectCell[1];

        return xyResultCell;
    }

    internal int[] GetXYDirect(DirectTypes direct)
    {
        var xyDirectCell = new int[XYForArray];

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