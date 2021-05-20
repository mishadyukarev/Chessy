using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;
using static MainGame;

internal class ShiftUnitMasterSystem : RPCMasterSystemReduction, IEcsRunSystem
{
    internal int[] XyPreviousCell => _eMM.MasterRPCEntXySelPreCom.XyPrevious;
    internal int[] XySelectedCell => _eMM.MasterRPCEntXySelPreCom.XySelected;
    internal PhotonMessageInfo Info => _eGM.GeneralRPCEntFromInfoCom.FromInfo;


    internal ShiftUnitMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public void Run()
    {
        List<int[]> xyAvailableCellsForShift = _eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).GetCellsForShift();

        if (_eGM.CellUnitEnt_OwnerCom(XyPreviousCell).IsHim(Info.Sender) && _eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).MinAmountSteps)
        {
            if (Instance.CellBaseOperations.TryFindCellInList(XySelectedCell, xyAvailableCellsForShift))
            {
                _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).SetUnit(XyPreviousCell);


                _eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).ResetUnit();


                _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).AmountSteps
                    -= _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).NeedAmountSteps;
                if (_eGM.CellUnitEnt_CellUnitCom(XySelectedCell).AmountSteps < 0) _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).AmountSteps = 0;

                _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).IsProtected = false;
                _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).IsRelaxed = false;
            }
        }
    }
}
