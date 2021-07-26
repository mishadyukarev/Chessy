using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Info;
using Photon.Pun;
using System.Collections.Generic;
using static Assets.Scripts.Workers.CellBaseOperations;

internal sealed class ShiftUnitMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.FromInfo;

    internal int[] FromXy => _eMM.ShiftEnt_FromToXyCom.FromXy;
    internal int[] ToXy => _eMM.ShiftEnt_FromToXyCom.ToXy;

    public override void Run()
    {
        base.Run();

        List<int[]> xyAvailableCellsForShift = CellUnitsDataWorker.GetCellsForShift(FromXy);

        if (CellUnitsDataWorker.IsHim(InfoFrom.Sender, FromXy) && CellUnitsDataWorker.HaveMinAmountSteps(FromXy))
        {
            if (xyAvailableCellsForShift.TryFindCell(ToXy))
            {
                PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);


                var fromUnitType = CellUnitsDataWorker.UnitType(FromXy);
                var fromIsMasterClient = CellUnitsDataWorker.IsMasterClient(FromXy);

                var fromCondition = CellUnitsDataWorker.ProtectRelaxType(FromXy);


                InfoUnitsWorker.TakeUnitInStandartCondition(fromCondition, fromUnitType, fromIsMasterClient, FromXy);

                InfoUnitsWorker.TakeAmountUnitInGame(CellUnitsDataWorker.UnitType(FromXy), CellUnitsDataWorker.IsMasterClient(FromXy), FromXy);
                InfoUnitsWorker.AddAmountUnitInGame(CellUnitsDataWorker.UnitType(FromXy), CellUnitsDataWorker.IsMasterClient(FromXy), ToXy);
                CellUnitsDataWorker.ShiftPlayerUnitToBaseCell(FromXy, ToXy);

                InfoUnitsWorker.AddUnitInStandartCondition(ConditionUnitTypes.None, fromUnitType, fromIsMasterClient, ToXy);


                CellUnitsDataWorker.TakeAmountSteps(ToXy, CellEnvirDataWorker.NeedAmountSteps(ToXy));
                if (CellUnitsDataWorker.AmountSteps(ToXy) < 0) CellUnitsDataWorker.ResetAmountSteps(ToXy);

                CellUnitsDataWorker.ResetProtectedRelaxType(ToXy);
            }
        }
    }
}
