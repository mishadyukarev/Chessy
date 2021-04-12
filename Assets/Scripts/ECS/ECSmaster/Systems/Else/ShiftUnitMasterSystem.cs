using Leopotam.Ecs;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;


public class ShiftUnitMasterSystem : CellReductionSystem, IEcsRunSystem
{
    private StartValuesConfig _startValues;

    private EcsComponentRef<ShiftUnitMasterComponent> _shiftComponentRef = default;
    private EcsComponentRef<UnitPathComponent> _unitPathComponentRef = default;


    internal ShiftUnitMasterSystem(ECSmanager eCSmanager, SupportManager supportManager) : base(eCSmanager, supportManager)
    {
        _shiftComponentRef = eCSmanager.EntitiesMasterManager.ShiftUnitComponentRef;
        _unitPathComponentRef = eCSmanager.EntitiesGeneralManager.UnitPathComponentRef;

        _startValues = supportManager.StartValues;
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
                CellUnitComponent(xyPreviousCell).TakeAmountSteps(_startValues.TakeAmountSteps);

                CellUnitComponent(xySelectedCell).SetResetUnit(CellUnitComponent(xyPreviousCell));
                CellUnitComponent(xySelectedCell).IsProtected = false;
                CellUnitComponent(xySelectedCell).IsRelaxed = false;

                CellUnitComponent(xyPreviousCell).ResetUnit();
            }
        }
    }
}
