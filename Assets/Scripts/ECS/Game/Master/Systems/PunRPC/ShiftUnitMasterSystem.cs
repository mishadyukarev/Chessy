using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Info;
using Photon.Pun;
using System.Collections.Generic;
using static Assets.Scripts.Workers.CellBaseOperations;

internal sealed class ShiftUnitMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;

    internal int[] FromXy => _eMM.ShiftEnt_FromToXyCom.FromXy;
    internal int[] ToXy => _eMM.ShiftEnt_FromToXyCom.ToXy;

    public override void Run()
    {
        base.Run();

        List<int[]> xyAvailableCellsForShift = CellUnitWorker.GetCellsForShift(FromXy);

        if (CellUnitWorker.IsHim(InfoFrom.Sender, FromXy) && CellUnitWorker.HaveMinAmountSteps(FromXy))
        {
            if (xyAvailableCellsForShift.TryFindCell(ToXy))
            {
                PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);


                var fromUnitType = CellUnitWorker.UnitType(FromXy);
                var fromIsMasterClient = CellUnitWorker.IsMasterClient(FromXy);

                var fromCondition = CellUnitWorker.ProtectRelaxType(FromXy);


                InfoUnitsWorker.TakeUnitInStandartCondition(fromCondition, fromUnitType, fromIsMasterClient, FromXy);

                InfoUnitsWorker.TakeAmountUnitInGame(CellUnitWorker.UnitType(FromXy), CellUnitWorker.IsMasterClient(FromXy), FromXy);
                InfoUnitsWorker.AddAmountUnitInGame(CellUnitWorker.UnitType(FromXy), CellUnitWorker.IsMasterClient(FromXy), ToXy);
                CellUnitWorker.ShiftPlayerUnitToBaseCell(FromXy, ToXy);

                InfoUnitsWorker.AddUnitInStandartCondition(ConditionTypes.None, fromUnitType, fromIsMasterClient, ToXy);


                CellUnitWorker.TakeAmountSteps(ToXy, CellEnvironmentWorker.NeedAmountSteps(ToXy));
                if (CellUnitWorker.AmountSteps(ToXy) < 0) CellUnitWorker.ResetAmountSteps(ToXy);

                CellUnitWorker.ResetProtectedRelaxType(ToXy);
            }
        }
    }
}
