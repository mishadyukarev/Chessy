using Leopotam.Ecs;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using static MainGame;

internal class ShiftUnitMasterSystem : RPCMasterSystemReduction, IEcsRunSystem
{
    internal int[] XyPreviousCell => _eMM.MasterRPCEntXySelPreCom.XyPrevious;
    internal int[] XySelectedCell => _eMM.MasterRPCEntXySelPreCom.XySelected;
    internal PhotonMessageInfo Info => _eGM.GeneralRPCEntFromInfoCom.FromInfo;


    internal ShiftUnitMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public void Run()
    {
        List<int[]> xyAvailableCellsForShift = InstanceGame.CellManager.CellFinderWay.GetCellsForShift(XyPreviousCell);

        if (_eGM.CellUnitComponent(XyPreviousCell).IsHisUnit(Info.Sender) && _eGM.CellUnitComponent(XyPreviousCell).MinAmountSteps)
        {
            if (InstanceGame.CellManager.CellBaseOperations.TryFindCellInList(XySelectedCell, xyAvailableCellsForShift))
            {
                _eGM.CellUnitComponent(XySelectedCell).SetUnit(_eGM.CellUnitComponent(XyPreviousCell));


                _eGM.CellUnitComponent(XyPreviousCell).ResetUnit();


                _eGM.CellUnitComponent(XySelectedCell).AmountSteps
                    -= _eGM.CellUnitComponent(XySelectedCell).NeedAmountSteps(_eGM.CellEnvironmentComponent(XySelectedCell).ListEnvironmentTypes);
                if (_eGM.CellUnitComponent(XySelectedCell).AmountSteps < 0) _eGM.CellUnitComponent(XySelectedCell).AmountSteps = 0;

                _eGM.CellUnitComponent(XySelectedCell).IsProtected = false;
                _eGM.CellUnitComponent(XySelectedCell).IsRelaxed = false;
            }
        }
    }
}
