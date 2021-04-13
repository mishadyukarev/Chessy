using Photon.Realtime;
using System.Collections.Generic;

public struct UnitPathComponent
{
    private StartValuesConfig _nameValueManager;
    private CellManager _cellManager;
    private SystemsGeneralManager _systemsGeneralManager;

    private UnitPathTypes _unitPathTypeIN;

    private int[] _xyStartCellIN;
    private Player _playerIN;

    private List<int[]> _xyAvailableCellsForShiftOUT;
    private List<int[]> _xyAvailableCellsForAttackOUT;



    public UnitPathComponent(SystemsGeneralManager systemsGeneralManager, StartValuesConfig nameValueManager, CellManager cellManager)
    {
        _nameValueManager = nameValueManager;
        _systemsGeneralManager = systemsGeneralManager;
        _cellManager = cellManager;

        _unitPathTypeIN = default;

        _xyStartCellIN = new int[nameValueManager.XY_FOR_ARRAY];
        _playerIN = default;

        _xyAvailableCellsForShiftOUT = new List<int[]>();
        _xyAvailableCellsForAttackOUT = new List<int[]>();
    }




    internal void Unpack(out UnitPathTypes unitPathTypeIN)
    {
        unitPathTypeIN = _unitPathTypeIN;
    }




    internal void GetAvailableCellsForShift(in int[] xyStartCellIN, in Player playerIN, out List<int[]> xyAvailableCellsForShiftOUT)
    {
        _xyAvailableCellsForShiftOUT.Clear();

        _unitPathTypeIN = UnitPathTypes.Shift;
        _cellManager.CopyXYinTo(xyStartCellIN, _xyStartCellIN);
        _playerIN = playerIN;

        InvokeSystem();

        xyAvailableCellsForShiftOUT = _cellManager.CopyListXY(_xyAvailableCellsForShiftOUT);
    }

    internal void UnpackForShift(out int[] xyStartCellIN, out Player playerIN)
    {
        xyStartCellIN = _xyStartCellIN;
        playerIN = _playerIN;
    }

    internal void PackForShift(in List<int[]> xyAvailableCellsForShiftOUT)
    {
        _xyAvailableCellsForShiftOUT = xyAvailableCellsForShiftOUT;
    }




    internal void GetAvailableCellsForAttack(in int[] xyStartCellIN, in Player playerIN, out List<int[]> xyAvailableCellsForAttackOUT)
    {
        _xyAvailableCellsForAttackOUT.Clear();

        _unitPathTypeIN = UnitPathTypes.Attack;
        _cellManager.CopyXYinTo(xyStartCellIN, _xyStartCellIN);
        _playerIN = playerIN;

        InvokeSystem();

        xyAvailableCellsForAttackOUT = _cellManager.CopyListXY(_xyAvailableCellsForAttackOUT);
    }

    internal void UnpackForAttack(out int[] xyStartCellIN, out Player playerIN)
    {
        xyStartCellIN = _xyStartCellIN;
        playerIN = _playerIN;
    }

    internal void PackForAttack(in List<int[]> xyAvailableCellsForAttack)
    {
        _xyAvailableCellsForAttackOUT = xyAvailableCellsForAttack;
    }


    private void InvokeSystem() => _systemsGeneralManager.InvokeRunSystem(SystemGeneralTypes.Else, nameof(UnitPathSystem));
}