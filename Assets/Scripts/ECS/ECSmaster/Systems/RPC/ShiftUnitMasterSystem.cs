﻿using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;

internal sealed class ShiftUnitMasterSystem : RPCMasterSystemReduction
{
    internal int[] XyPreviousCell => _eMM.RPCMasterEnt_RPCMasterCom.XyPrevious;
    internal int[] XySelectedCell => _eMM.RPCMasterEnt_RPCMasterCom.XySelected;
    internal PhotonMessageInfo Info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;


    public override void Run()
    {
        base.Run();

        List<int[]> xyAvailableCellsForShift = _cM.CellUnitWorker.GetCellsForShift(XyPreviousCell);

        if (_eGM.CellUnitEnt_CellOwnerCom(XyPreviousCell).IsHim(Info.Sender) && _eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).HaveMinAmountSteps)
        {
            if (_cM.CellBaseOperations.TryFindCellInList(XySelectedCell, xyAvailableCellsForShift))
            {
                _cM.CellUnitWorker.SetUnit(XyPreviousCell, XySelectedCell);


                _cM.CellUnitWorker.ResetUnit(XyPreviousCell);


                _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).AmountSteps
                    -= _cM.CellUnitWorker.NeedAmountSteps(XySelectedCell);
                if (_eGM.CellUnitEnt_CellUnitCom(XySelectedCell).AmountSteps < 0) _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).AmountSteps = 0;

                _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).IsProtected = false;
                _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).IsRelaxed = false;
            }
        }
    }
}
