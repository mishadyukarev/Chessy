using Leopotam.Ecs;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;


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

        _unitPathComponentRef.Unref().GetAvailableCellsForShift(xyPreviousCell, fromPlayer, out List<int[]> xyAvailableCellsForShift);

        if (CellUnitComponent(xyPreviousCell).IsHim(fromPlayer) && CellUnitComponent(xyPreviousCell).HaveAmountSteps)
        {
            if (_cellManager.TryFindCellInList(xySelectedCell, xyAvailableCellsForShift))
            {
                CellUnitComponent(xyPreviousCell).TakeAmountSteps(_nameValueManager.TAKE_AMOUNT_STEPS);

                CellUnitComponent(xySelectedCell).SetResetUnit(CellUnitComponent(xyPreviousCell));
                CellUnitComponent(xySelectedCell).IsProtected = false;
                CellUnitComponent(xySelectedCell).IsRelaxed = false;

                CellUnitComponent(xyPreviousCell).ResetUnit();
            }
        }
    }
}
