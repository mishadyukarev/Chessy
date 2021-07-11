using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;
using System.Collections.Generic;
using static Assets.Scripts.Static.CellBaseOperations;

internal sealed class ShiftUnitMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;

    internal int[] XyPreviousCell => _eMM.RPCMasterEnt_RPCMasterCom.XyPrevious;
    internal int[] XySelectedCell => _eMM.RPCMasterEnt_RPCMasterCom.XySelected;

    public override void Run()
    {
        base.Run();

        List<int[]> xyAvailableCellsForShift = CellUnitWorker.GetCellsForShift(XyPreviousCell);

        if (_eGM.CellUnitEnt_CellOwnerCom(XyPreviousCell).IsHim(InfoFrom.Sender) && _eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).HaveMinAmountSteps)
        {
            if (TryFindCellInList(XySelectedCell, xyAvailableCellsForShift))
            {
                _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                CellUnitWorker.ShiftUnit(XyPreviousCell, XySelectedCell);


                if (_eGM.CellUnitEnt_CellOwnerCom(XyPreviousCell).HaveOwner)
                {
                    CellUnitWorker.ResetPlayerUnit(XyPreviousCell);
                }
                else
                {
                    CellUnitWorker.ResetBotUnit(XyPreviousCell);
                }


                _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).TakeAmountSteps(_eGM.CellEnvEnt_CellEnvCom(XySelectedCell).NeedAmountSteps());
                if (_eGM.CellUnitEnt_CellUnitCom(XySelectedCell).AmountSteps < 0) _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).ResetAmountSteps();

                _eGM.CellUnitEnt_ProtectRelaxCom(XySelectedCell).ResetProtectedRelaxedType();
            }
        }
    }
}
