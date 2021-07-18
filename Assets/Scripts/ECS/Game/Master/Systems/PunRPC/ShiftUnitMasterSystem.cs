using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;
using System.Collections.Generic;
using static Assets.Scripts.Static.CellBaseOperations;

internal sealed class ShiftUnitMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;

    internal int[] FromXy => _eMM.ShiftEnt_FromToXyCom.FromXy;
    internal int[] ToXy => _eMM.ShiftEnt_FromToXyCom.ToXy;

    public override void Run()
    {
        base.Run();

        List<int[]> xyAvailableCellsForShift = CellUnitWorker.GetCellsForShift(FromXy);

        if (_eGM.CellUnitEnt_CellOwnerCom(FromXy).IsHim(InfoFrom.Sender) && _eGM.CellUnitEnt_CellUnitCom(FromXy).HaveMinAmountSteps)
        {
            if (TryFindCellInList(ToXy, xyAvailableCellsForShift))
            {
                _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

                CellUnitWorker.ShiftPlayerUnit(FromXy, ToXy);


                _eGM.CellUnitEnt_CellUnitCom(ToXy).TakeAmountSteps(_eGM.CellEnvEnt_CellEnvCom(ToXy).NeedAmountSteps());
                if (_eGM.CellUnitEnt_CellUnitCom(ToXy).AmountSteps < 0) _eGM.CellUnitEnt_CellUnitCom(ToXy).ResetAmountSteps();

                _eGM.CellUnitEnt_ProtectRelaxCom(ToXy).ResetProtectedRelaxedType();
            }
        }
    }
}
