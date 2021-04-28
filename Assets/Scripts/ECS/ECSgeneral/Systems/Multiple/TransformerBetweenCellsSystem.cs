using Leopotam.Ecs;
using System.Collections.Generic;
using static MainGame;


internal struct TransformerBetweenCellsComponent
{
    private ForUnitPathTypes _forUnitPathTypeIN;
    private bool _isFromStartIN;
    private int[] _xyStartCellIN;

    private int[] _xyCurrentCellOUT;
    private List<int[]> _xyCurrentCellsOUT;

    private SystemsGeneralManager _systemsGeneralManager;

    public TransformerBetweenCellsComponent(ECSmanager eCSmanager)
    {
        _forUnitPathTypeIN = default;
        _isFromStartIN = default;
        _xyStartCellIN = new int[InstanceGame.StartValuesGameConfig.XY_FOR_ARRAY];

        _xyCurrentCellOUT = new int[InstanceGame.StartValuesGameConfig.XY_FOR_ARRAY];
        _xyCurrentCellsOUT = new List<int[]>();

        _systemsGeneralManager = eCSmanager.SystemsGeneralManager;
    }

    internal void Unpack(out ForUnitPathTypes forUnitPathTypeIN, out bool isFromStartIN, out int[] xyStartCellIN)
    {
        forUnitPathTypeIN = _forUnitPathTypeIN;
        isFromStartIN = _isFromStartIN;
        xyStartCellIN = _xyStartCellIN;
    }


    public int[] TryGetXYCurrentCell(ForUnitPathTypes ForUnitPathType, bool isFromStartIN, int[] xyStartCellIN)
    {
        _forUnitPathTypeIN = ForUnitPathType;
        _isFromStartIN = isFromStartIN;
        InstanceGame.CellManager.CopyXYinTo(xyStartCellIN, _xyStartCellIN);

        _systemsGeneralManager.InvokeRunSystem(SystemGeneralTypes.Multiple, nameof(TransformerBetweenCellsSystem));

        return InstanceGame.CellManager.CopyXY(_xyCurrentCellOUT);
    }

    internal void Pack(int[] xyCurrentCellOUT)
    {
        _xyCurrentCellOUT = xyCurrentCellOUT;
    }


    public List<int[]> TryGetXYCurrentCells(bool isFromStartIN, int[] xyStartCellIN, ForUnitPathTypes ForUnitPathType = ForUnitPathTypes.Around)
    {
        _forUnitPathTypeIN = ForUnitPathType;
        _isFromStartIN = isFromStartIN;
        InstanceGame.CellManager.CopyXYinTo(xyStartCellIN, _xyStartCellIN);

        _systemsGeneralManager.InvokeRunSystem(SystemGeneralTypes.Multiple, nameof(TransformerBetweenCellsSystem));

        return InstanceGame.CellManager.CopyListXY(_xyCurrentCellsOUT);
    }

    internal void Pack(List<int[]> xyCurrentCellOUT)
    {
        _xyCurrentCellsOUT = xyCurrentCellOUT;
    }
}



internal class TransformerBetweenCellsSystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<TransformerBetweenCellsComponent> _transformerBetweenCellsComponentRef = default;

    internal TransformerBetweenCellsSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _transformerBetweenCellsComponentRef = eCSmanager.EntitiesGeneralManager.TransformerBetweenCellsComponentRef;
    }


    public void Run()
    {
        _transformerBetweenCellsComponentRef.Unref()
            .Unpack(out ForUnitPathTypes forUnitPathTypeIN, out bool isFromStartIN, out int[] xyStartCellIN);


        if(forUnitPathTypeIN == ForUnitPathTypes.Around)
        {
            var xyAvailableCellsForShift = new List<int[]>();

            for (int i = 0; i < (int)ForUnitPathTypes.LeftDown + 1; i++)
            {
                GetCurrentCell(true, (ForUnitPathTypes)i, xyStartCellIN, out var xyCurrentCellForShift);

                xyAvailableCellsForShift.Add(xyCurrentCellForShift);
            }

            _transformerBetweenCellsComponentRef.Unref().Pack(xyAvailableCellsForShift);
        }

        else
        {
            GetCurrentCell(isFromStartIN, forUnitPathTypeIN, xyStartCellIN, out int[] xyCurrentCell);
            _transformerBetweenCellsComponentRef.Unref().Pack(xyCurrentCell);
        }
    }

    private void GetCurrentCell(bool isFromStart, ForUnitPathTypes unitPathType, int[] xyStartCell, out int[] xyCurrentCellForAll)
    {
        xyCurrentCellForAll = new int[InstanceGame.StartValuesGameConfig.XY_FOR_ARRAY];
        var changeXY = new int[InstanceGame.StartValuesGameConfig.XY_FOR_ARRAY];

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
