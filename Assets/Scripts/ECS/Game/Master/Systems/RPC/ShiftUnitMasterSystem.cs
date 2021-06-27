using Assets.Scripts;
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

        List<int[]> xyAvailableCellsForShift = _cellM.CellUnitWorker.GetCellsForShift(XyPreviousCell);

        if (_eGM.CellUnitEnt_CellOwnerCom(XyPreviousCell).IsHim(Info.Sender) && _eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).HaveMinAmountSteps)
        {
            if (_cellM.CellBaseOperations.TryFindCellInList(XySelectedCell, xyAvailableCellsForShift))
            {
                _cellM.CellUnitWorker.SetUnit(XyPreviousCell, XySelectedCell);


                _cellM.CellUnitWorker.ResetUnit(XyPreviousCell);


                _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).AmountSteps
                    -= _cellM.CellUnitWorker.NeedAmountSteps(XySelectedCell);
                if (_eGM.CellUnitEnt_CellUnitCom(XySelectedCell).AmountSteps < 0) _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).AmountSteps = 0;

                _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).IsProtected = false;
                _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).IsRelaxed = false;
            }
        }
    }
}
