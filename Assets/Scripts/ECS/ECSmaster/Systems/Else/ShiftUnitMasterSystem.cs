using Leopotam.Ecs;
using Photon.Realtime;
using System.Collections.Generic;


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
