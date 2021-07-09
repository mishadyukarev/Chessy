﻿using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
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
                _photonPunRPC.SoundToGeneral(Info.Sender, SoundEffectTypes.ClickToTable);

                CellUnitWorker.ShiftUnit(XyPreviousCell, XySelectedCell);


                CellUnitWorker.ResetUnit(XyPreviousCell);
     

                _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).TakeAmountSteps(_eGM.CellEnvEnt_CellEnvCom(XySelectedCell).NeedAmountSteps());
                if (_eGM.CellUnitEnt_CellUnitCom(XySelectedCell).AmountSteps < 0) _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).ResetAmountSteps();

                _eGM.CellUnitEnt_ProtectRelaxCom(XySelectedCell).ResetProtectedRelaxedType();
            }
        }
    }
}
