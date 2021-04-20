using Leopotam.Ecs;
using Photon.Realtime;
using System.Collections.Generic;


public class ShiftUnitMasterSystem : CellReductionSystem, IEcsRunSystem
{
    private EcsComponentRef<ShiftUnitMasterComponent> _shiftComponentRef = default;
    private EcsComponentRef<UnitPathComponent> _unitPathComponentRef = default;


    internal ShiftUnitMasterSystem(ECSmanager eCSmanager, SupportManager supportManager) : base(eCSmanager, supportManager)
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
                CellUnitComponent(xyPreviousCell).AmountSteps -= _startValues.TAKE_AMOUNT_STEPS;

                CellUnitComponent(xySelectedCell).SetUnit(CellUnitComponent(xyPreviousCell));
                CellUnitComponent(xySelectedCell).IsProtected = false;
                CellUnitComponent(xySelectedCell).IsRelaxed = false;

                CellUnitComponent(xyPreviousCell).ResetUnit();
            }
        }
    }
}
