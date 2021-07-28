using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Game.Else.Info.Units;
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

                var fromCondition = CellUnitsDataWorker.ConditionType(FromXy);


                InfoUnitsConditionWorker.RemoveUnitInCondition(fromCondition, fromUnitType, fromIsMasterClient, FromXy);

                InfoAmountUnitsWorker.RemoveAmountUnitsInGame(CellUnitsDataWorker.UnitType(FromXy), CellUnitsDataWorker.IsMasterClient(FromXy), FromXy);
                InfoAmountUnitsWorker.AddAmountUnitInGame(CellUnitsDataWorker.UnitType(FromXy), CellUnitsDataWorker.IsMasterClient(FromXy), ToXy);
                CellUnitsDataWorker.ShiftPlayerUnitToBaseCell(FromXy, ToXy);

                InfoUnitsConditionWorker.AddUnitInCondition(ConditionUnitTypes.None, fromUnitType, fromIsMasterClient, ToXy);


                CellUnitsDataWorker.TakeAmountSteps(ToXy, CellEnvirDataWorker.NeedAmountSteps(ToXy));
                if (CellUnitsDataWorker.AmountSteps(ToXy) < 0) CellUnitsDataWorker.ResetAmountSteps(ToXy);

                CellUnitsDataWorker.ResetConditionType(ToXy);
            }
        }
    }
}
