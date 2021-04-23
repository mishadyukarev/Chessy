using Leopotam.Ecs;
using Photon.Realtime;
using System.Collections.Generic;

public struct ShiftUnitMasterComponent
{
    private CellManager _cellManager;
    private SystemsMasterManager _systemsMasterManager;

    private int[] _xyPreviousCellIN;
    private int[] _xySelectedCellIN;
    private Player _playerIN;

    private bool _isShiftedOUT;


    public ShiftUnitMasterComponent(StartValuesGameConfig nameValueManager, CellManager cellManager, SystemsMasterManager systemsMasterManager)
    {
        _cellManager = cellManager;
        _systemsMasterManager = systemsMasterManager;

        _xyPreviousCellIN = new int[nameValueManager.XY_FOR_ARRAY];
        _xySelectedCellIN = new int[nameValueManager.XY_FOR_ARRAY];
        _playerIN = default;

        _isShiftedOUT = default;
    }


    public bool ShiftUnit(int[] xyPreviousCell, int[] xySelectedCell, Player player)
    {
        _cellManager.CopyXYinTo(xyPreviousCell, _xyPreviousCellIN);
        _cellManager.CopyXYinTo(xySelectedCell, _xySelectedCellIN);
        _playerIN = player;

        _systemsMasterManager.InvokeRunSystem(SystemMasterTypes.Else, nameof(ShiftUnitMasterSystem));

        return _isShiftedOUT;
    }

    public void Unpack(out int[] xyPreviousCell, out int[] xySelectedCell, out Player player)
    {
        xyPreviousCell = _xyPreviousCellIN;
        xySelectedCell = _xySelectedCellIN;
        player = _playerIN;
    }

    public void Pack(bool isShifted)
    {
        _isShiftedOUT = isShifted;
    }
}


public class ShiftUnitMasterSystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<ShiftUnitMasterComponent> _shiftComponentRef = default;
    private EcsComponentRef<UnitPathsComponent> _unitPathComponentRef = default;


    internal ShiftUnitMasterSystem(ECSmanager eCSmanager, SupportGameManager supportManager) : base(eCSmanager, supportManager)
    {
        _shiftComponentRef = eCSmanager.EntitiesMasterManager.ShiftUnitComponentRef;
        _unitPathComponentRef = eCSmanager.EntitiesGeneralManager.UnitPathComponentRef;
    }


    public void Run()
    {
        _shiftComponentRef.Unref().Pack(false);

        _shiftComponentRef.Unref().Unpack(out int[] xyPreviousCell, out int[] xySelectedCell, out Player fromPlayer);

        List<int[]> xyAvailableCellsForShift = _unitPathComponentRef.Unref().GetAvailableCellsForShift(xyPreviousCell, fromPlayer);

        if (CellUnitComponent(xyPreviousCell).IsHim(fromPlayer) && CellUnitComponent(xyPreviousCell).HaveAmountSteps)
        {
            if (_cellManager.TryFindCellInList(xySelectedCell, xyAvailableCellsForShift))
            {
                CellUnitComponent(xyPreviousCell).AmountSteps -= _startValues.AMOUNT_FOR_TAKE_UNIT;

                CellUnitComponent(xySelectedCell).SetUnit(CellUnitComponent(xyPreviousCell));
                CellUnitComponent(xySelectedCell).IsProtected = false;
                CellUnitComponent(xySelectedCell).IsRelaxed = false;

                CellUnitComponent(xyPreviousCell).ResetUnit();
            }
        }
    }
}
