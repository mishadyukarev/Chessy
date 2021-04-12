using Leopotam.Ecs;
using Photon.Realtime;
using System.Collections.Generic;

public partial class UnitPathSystem : CellReductionSystem, IEcsInitSystem, IEcsRunSystem
{
    private int[] _xyCurrentCell = default;
    private int[] _changeXY = default;

    private EcsComponentRef<UnitPathComponent> _unitPathComponentRef;


    internal UnitPathSystem(ECSmanager eCSmanager, SupportManager supportManager) : base(eCSmanager, supportManager)
    {
        _unitPathComponentRef = eCSmanager.EntitiesGeneralManager.UnitPathComponentRef;
    }


    public void Init()
    {
        _xyCurrentCell = new int[_nameValueManager.XY_FOR_ARRAY];
        _changeXY = new int[_nameValueManager.XY_FOR_ARRAY];
    }

    public void Run()
    {
        _unitPathComponentRef.Unref().Unpack(out UnitPathTypes unitPathTypeIN);

        int[] xyStartCellIN;
        Player playerIN;

        switch (unitPathTypeIN)
        {
            case UnitPathTypes.Shift:

                _unitPathComponentRef.Unref().UnpackForShift(out xyStartCellIN, out playerIN);
                switch (CellUnitComponent(xyStartCellIN).UnitType)
                {
                    case UnitTypes.None:
                        break;

                    case UnitTypes.King:
                        break;

                    case UnitTypes.Pawn:
                        RealizePawnPathForShift(xyStartCellIN, playerIN);
                        break;

                    default:
                        break;
                }

                break;

            case UnitPathTypes.Attack:

                _unitPathComponentRef.Unref().UnpackForAttack(out xyStartCellIN, out playerIN);
                switch (CellUnitComponent(xyStartCellIN).UnitType)
                {
                    case UnitTypes.None:
                        break;

                    case UnitTypes.King:
                        break;

                    case UnitTypes.Pawn:
                        RealizePawnPathForAttack(xyStartCellIN, playerIN);
                        break;

                    default:
                        break;
                }

                break;

            default:
                break;
        }
    }

    private void RealizePawnPathForShift(int[] xyStartCellIN, Player playerIN)
    {
        var xyAvailableCellsForShift = new List<int[]>();

        for (int i = 0; i < (int)ForUnitPathTypes.LeftDown + 1; i++)
        {
            GetCurrentCell(true, (ForUnitPathTypes)i, xyStartCellIN, out var xyCurrentCellForShift);

            if (!CellEnvironmentComponent(xyCurrentCellForShift).HaveMountain)
            {
                xyAvailableCellsForShift.Add(xyCurrentCellForShift);
            }
        }

        _unitPathComponentRef.Unref().PackForShift(xyAvailableCellsForShift);
    }

    private void RealizePawnPathForAttack(int[] xyStartCellIN, Player playerIN)
    {
        var xyAvailableCellsForAttack = new List<int[]>();

        for (int i = 0; i < (int)ForUnitPathTypes.LeftDown + 1; i++)
        {
            GetCurrentCell(true, (ForUnitPathTypes)i, xyStartCellIN, out var xyCurrentCellForShift);

            if (!CellEnvironmentComponent(xyCurrentCellForShift).HaveMountain)
            {
                if (CellUnitComponent(xyCurrentCellForShift).HaveUnit)
                {
                    if (playerIN.ActorNumber != CellUnitComponent(xyCurrentCellForShift).ActorNumber)
                    {
                        xyAvailableCellsForAttack.Add(xyCurrentCellForShift);
                    }
                }
            }
        }

        _unitPathComponentRef.Unref().PackForAttack(xyAvailableCellsForAttack);
    }

    private void GetCurrentCell(bool isFromStart, ForUnitPathTypes unitPathType, int[] xyStartCell, out int[] xyCurrentCellForAll)
    {
        xyCurrentCellForAll = _cellManager.CopyXY(_xyCurrentCell);
        var changeXY = _changeXY;

        switch (unitPathType)
        {
            case ForUnitPathTypes.Right:
                changeXY[X] = 1;
                changeXY[Y] = 0;
                break;

            case ForUnitPathTypes.Left:
                changeXY[X] = -1;
                changeXY[Y] = 0;
                break;

            case ForUnitPathTypes.Up:
                changeXY[X] = 0;
                changeXY[Y] = 1;
                break;

            case ForUnitPathTypes.Down:
                changeXY[X] = 0;
                changeXY[Y] = -1;
                break;

            case ForUnitPathTypes.RightUp:
                changeXY[X] = 1;
                changeXY[Y] = 1;
                break;

            case ForUnitPathTypes.LeftUp:
                changeXY[X] = -1;
                changeXY[Y] = 1;
                break;

            case ForUnitPathTypes.RightDown:
                changeXY[X] = 1;
                changeXY[Y] = -1;
                break;

            case ForUnitPathTypes.LeftDown:
                changeXY[X] = -1;
                changeXY[Y] = -1;
                break;

            default:
                break;
        }

        if (isFromStart)
        {
            xyCurrentCellForAll[X] = xyStartCell[X] + changeXY[X];
            xyCurrentCellForAll[Y] = xyStartCell[Y] + changeXY[Y];
        }
        else
        {
            xyCurrentCellForAll[X] += changeXY[X];
            xyCurrentCellForAll[Y] += changeXY[Y];
        }
    }

}
