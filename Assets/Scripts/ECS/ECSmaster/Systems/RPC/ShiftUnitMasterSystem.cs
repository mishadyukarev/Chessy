using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;
using static MainGame;

internal class ShiftUnitMasterSystem : RPCMasterSystemReduction, IEcsRunSystem
{
    internal int[] XyPreviousCell => _eMM.RPCMasterEnt_RPCMasterCom.XyPrevious;
    internal int[] XySelectedCell => _eMM.RPCMasterEnt_RPCMasterCom.XySelected;
    internal PhotonMessageInfo Info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;


    internal ShiftUnitMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public void Run()
    {
        List<int[]> xyAvailableCellsForShift = _eGM.CellEnt_CellUnitCom(XyPreviousCell).GetCellsForShift();

        if (_eGM.CellEnt_CellUnitCom(XyPreviousCell).IsHim(Info.Sender) && _eGM.CellEnt_CellUnitCom(XyPreviousCell).MinAmountSteps)
        {
            if (_eGM.CellBaseOperEnt_CellBaseOperCom.TryFindCellInList(XySelectedCell, xyAvailableCellsForShift))
            {
                _eGM.CellEnt_CellUnitCom(XySelectedCell).SetUnit(XyPreviousCell);


                _eGM.CellEnt_CellUnitCom(XyPreviousCell).ResetUnit();


                _eGM.CellEnt_CellUnitCom(XySelectedCell).AmountSteps
                    -= _eGM.CellEnt_CellUnitCom(XySelectedCell).NeedAmountSteps;
                if (_eGM.CellEnt_CellUnitCom(XySelectedCell).AmountSteps < 0) _eGM.CellEnt_CellUnitCom(XySelectedCell).AmountSteps = 0;

                _eGM.CellEnt_CellUnitCom(XySelectedCell).IsProtected = false;
                _eGM.CellEnt_CellUnitCom(XySelectedCell).IsRelaxed = false;
            }
        }
    }
}
