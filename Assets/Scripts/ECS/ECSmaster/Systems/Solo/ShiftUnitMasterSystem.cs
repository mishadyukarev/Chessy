using Leopotam.Ecs;
using Photon.Realtime;
using System.Collections.Generic;
using static MainGame;

public struct ShiftUnitMasterComponent
{
    private CellBaseOperations _cellManager;
    private SystemsMasterManager _systemsMasterManager;

    private int[] _xyPreviousCellIN;
    private int[] _xySelectedCellIN;
    private Player _playerIN;

    private bool _isShiftedOUT;


    public ShiftUnitMasterComponent(StartValuesGameConfig nameValueManager, CellBaseOperations cellManager, SystemsMasterManager systemsMasterManager)
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

        _systemsMasterManager.InvokeRunSystem(SystemMasterTypes.Multiple, nameof(ShiftUnitMasterSystem));

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

    private PhotonPunRPC _photonPunRPC = default;


    internal ShiftUnitMasterSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _shiftComponentRef = eCSmanager.EntitiesMasterManager.ShiftUnitComponentRef;

        _photonPunRPC = InstanceGame.PhotonGameManager.PhotonPunRPC;
    }


    public void Run()
    {
        _shiftComponentRef.Unref().Pack(false);

        _shiftComponentRef.Unref().Unpack(out int[] xyPreviousCell, out int[] xySelectedCell, out Player fromPlayer);

        List<int[]> xyAvailableCellsForShift = InstanceGame.CellManager.CellFinderWay.GetCellsForShift(xyPreviousCell);

        if (CellUnitComponent(xyPreviousCell).IsHisUnit(fromPlayer) && CellUnitComponent(xyPreviousCell).MinAmountSteps)
        {
            if (_cellManager.TryFindCellInList(xySelectedCell, xyAvailableCellsForShift))
            {
                CellUnitComponent(xySelectedCell).SetUnit(CellUnitComponent(xyPreviousCell));


                CellUnitComponent(xyPreviousCell).ResetUnit();


                CellUnitComponent(xySelectedCell).AmountSteps
                    -= CellUnitComponent(xySelectedCell).NeedAmountSteps(CellEnvironmentComponent(xySelectedCell).ListEnvironmentTypes);
                if (CellUnitComponent(xySelectedCell).AmountSteps < 0) CellUnitComponent(xySelectedCell).AmountSteps = 0;

                CellUnitComponent(xySelectedCell).IsProtected = false;
                CellUnitComponent(xySelectedCell).IsRelaxed = false;
            }
        }
    }
}
