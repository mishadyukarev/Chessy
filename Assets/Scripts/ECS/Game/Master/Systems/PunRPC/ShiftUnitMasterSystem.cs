using Assets.Scripts;
using Photon.Pun;
using System.Collections.Generic;
using static Assets.Scripts.Static.CellBaseOperations;

internal sealed class ShiftUnitMasterSystem : RPCMasterSystemReduction
{
    internal int[] XyPreviousCell => _eMM.RPCMasterEnt_RPCMasterCom.XyPrevious;
    internal int[] XySelectedCell => _eMM.RPCMasterEnt_RPCMasterCom.XySelected;
    internal PhotonMessageInfo Info => _eGM.RpcGeneralEnt_RPCCom.FromInfo;


    public override void Run()
    {
        base.Run();

        List<int[]> xyAvailableCellsForShift = CellUnitWorker.GetCellsForShift(XyPreviousCell);

        if (_eGM.CellUnitEnt_CellOwnerCom(XyPreviousCell).IsHim(Info.Sender) && _eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).HaveMinAmountSteps)
        {
            if (TryFindCellInList(XySelectedCell, xyAvailableCellsForShift))
            {
                CellUnitWorker.ShiftUnit(XyPreviousCell, XySelectedCell);


                CellUnitWorker.ResetUnit(XyPreviousCell);


                _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).AmountSteps
                    -= CellUnitWorker.NeedAmountSteps(XySelectedCell);
                if (_eGM.CellUnitEnt_CellUnitCom(XySelectedCell).AmountSteps < 0) _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).AmountSteps = 0;

                _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).IsProtected = false;
                _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).IsRelaxed = false;
            }
        }
    }
}
