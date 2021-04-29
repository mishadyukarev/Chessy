using Leopotam.Ecs;
using Photon.Realtime;
using System.Collections.Generic;


public struct UnitPathsComponent
{
    private CellManager _cellManager;
    private SystemsGeneralManager _systemsGeneralManager;

    private UnitPathTypes _unitPathTypeIN;
    private int[] _xyStartCellIN;
    private Player _playerIN;

    private List<int[]> _xyAvailableCellsOUT;

    public UnitPathsComponent(SystemsGeneralManager systemsGeneralManager, StartValuesGameConfig nameValueManager, CellManager cellManager)
    {
        _systemsGeneralManager = systemsGeneralManager;
        _cellManager = cellManager;

        _unitPathTypeIN = default;

        _xyStartCellIN = new int[nameValueManager.XY_FOR_ARRAY];
        _playerIN = default;

        _xyAvailableCellsOUT = new List<int[]>();
    }



    internal List<int[]> GetAvailableCells(UnitPathTypes unitPathType, in int[] xyStartCellIN, in Player playerIN)
    {
        _xyAvailableCellsOUT.Clear();

        _unitPathTypeIN = unitPathType;
        _cellManager.CopyXYinTo(xyStartCellIN, _xyStartCellIN);
        _playerIN = playerIN;

        _systemsGeneralManager.InvokeRunSystem(SystemGeneralTypes.Multiple, nameof(UnitPathSystem));

        return _cellManager.CopyListXY(_xyAvailableCellsOUT);
    }

    internal void Unpack(out UnitPathTypes unitPathTypeIN, out int[] xyStartCellIN, out Player playerIN)
    {
        unitPathTypeIN = _unitPathTypeIN;
        xyStartCellIN = _xyStartCellIN;
        playerIN = _playerIN;
    }

    internal void Pack(in List<int[]> xyAvailableCellsOUT)
    {
        _xyAvailableCellsOUT = xyAvailableCellsOUT;
    }
}



public partial class UnitPathSystem : CellReduction, IEcsInitSystem, IEcsRunSystem
{
    private int[] _xyCurrentCell = default;
    private int[] _changeXY = default;

    private EcsComponentRef<UnitPathsComponent> _unitPathComponentRef;


    internal UnitPathSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _unitPathComponentRef = eCSmanager.EntitiesGeneralManager.UnitPathComponentRef;
    }


    public void Init()
    {
        _xyCurrentCell = new int[_startValuesGameConfig.XY_FOR_ARRAY];
        _changeXY = new int[_startValuesGameConfig.XY_FOR_ARRAY];
    }

    public void Run()
    {
        _unitPathComponentRef.Unref().Unpack(out UnitPathTypes unitPathTypeIN, out int[] xyStartCellIN, out Player playerIN);

        switch (unitPathTypeIN)
        {
            case UnitPathTypes.Shift:

                var xyAvailableCellsForShift = new List<int[]>();

                for (int i = 0; i < (int)DirectTypes.LeftDown + 1; i++)
                {
                    GetCurrentCell(true, (DirectTypes)i, xyStartCellIN, out var xyCurrentCellForShift);

                    if (!CellEnvironmentComponent(xyCurrentCellForShift).HaveMountain)
                    {
                        if (CellUnitComponent(xyStartCellIN).AmountSteps >= CellEnvironmentComponent(xyCurrentCellForShift).NeedAmountSteps + _startValuesGameConfig.MIN_AMOUNT_STEPS_FOR_UNIT
                            || CellUnitComponent(xyStartCellIN).HaveMaxSteps)
                        {
                            xyAvailableCellsForShift.Add(xyCurrentCellForShift);
                        }
                    }
                }

                _unitPathComponentRef.Unref().Pack(xyAvailableCellsForShift);

                break;

            case UnitPathTypes.Attack:

                var xyAvailableCellsForAttack = new List<int[]>();

                for (int i = 0; i < (int)DirectTypes.LeftDown + 1; i++)
                {
                    GetCurrentCell(true, (DirectTypes)i, xyStartCellIN, out var xyCurrentCellForShift);

                    if (!CellEnvironmentComponent(xyCurrentCellForShift).HaveMountain)
                    {
                        if (CellUnitComponent(xyCurrentCellForShift).HaveUnit)
                        {
                            if (CellUnitComponent(xyStartCellIN).AmountSteps >= CellEnvironmentComponent(xyCurrentCellForShift).NeedAmountSteps + _startValuesGameConfig.MIN_AMOUNT_STEPS_FOR_UNIT
                                || CellUnitComponent(xyStartCellIN).HaveMaxSteps)
                            {
                                if (playerIN.ActorNumber != CellUnitComponent(xyCurrentCellForShift).ActorNumber)
                                {
                                    xyAvailableCellsForAttack.Add(xyCurrentCellForShift);
                                }
                            }
                        }
                    }
                }

                _unitPathComponentRef.Unref().Pack(xyAvailableCellsForAttack);

                break;

            default:
                break;
        }
    }

    private void GetCurrentCell(bool isFromStart, DirectTypes unitPathType, int[] xyStartCell, out int[] xyCurrentCellForAll)
    {
        xyCurrentCellForAll = _cellManager.CopyXY(_xyCurrentCell);
        var changeXY = _changeXY;

        switch (unitPathType)
        {
            case DirectTypes.Right:
                changeXY[X] = 1;
                changeXY[Y] = 0;
                break;

            case DirectTypes.Left:
                changeXY[X] = -1;
                changeXY[Y] = 0;
                break;

            case DirectTypes.Up:
                changeXY[X] = 0;
                changeXY[Y] = 1;
                break;

            case DirectTypes.Down:
                changeXY[X] = 0;
                changeXY[Y] = -1;
                break;

            case DirectTypes.RightUp:
                changeXY[X] = 1;
                changeXY[Y] = 1;
                break;

            case DirectTypes.LeftUp:
                changeXY[X] = -1;
                changeXY[Y] = 1;
                break;

            case DirectTypes.RightDown:
                changeXY[X] = 1;
                changeXY[Y] = -1;
                break;

            case DirectTypes.LeftDown:
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
