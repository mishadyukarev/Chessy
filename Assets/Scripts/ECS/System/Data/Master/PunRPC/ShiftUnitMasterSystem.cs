using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using System.Collections.Generic;
using static Assets.Scripts.Workers.CellBaseOperations;

internal sealed class ShiftUnitMasterSystem : SystemMasterReduction
{
    internal int[] FromXy => _eMM.ShiftEnt_FromToXyCom.FromXy;
    internal int[] ToXy => _eMM.ShiftEnt_FromToXyCom.ToXy;

    public override void Run()
    {
        base.Run();

        List<int[]> xyAvailableCellsForShift = CellUnitsDataContainer.GetCellsForShift(FromXy);

        if (CellUnitsDataContainer.IsHim(RpcMasterDataContainer.InfoFrom.Sender, FromXy) && CellUnitsDataContainer.HaveMinAmountSteps(FromXy))
        {
            if (xyAvailableCellsForShift.TryFindCell(ToXy))
            {
                PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.ClickToTable);


                var fromUnitType = CellUnitsDataContainer.UnitType(FromXy);
                var fromIsMasterClient = CellUnitsDataContainer.IsMasterClient(FromXy);

                var fromCondition = CellUnitsDataContainer.ConditionType(FromXy);


                InfoUnitsDataContainer.RemoveUnitInCondition(fromCondition, fromUnitType, fromIsMasterClient, FromXy);

                InfoUnitsDataContainer.RemoveAmountUnitsInGame(CellUnitsDataContainer.UnitType(FromXy), CellUnitsDataContainer.IsMasterClient(FromXy), FromXy);
                InfoUnitsDataContainer.AddAmountUnitInGame(CellUnitsDataContainer.UnitType(FromXy), CellUnitsDataContainer.IsMasterClient(FromXy), ToXy);
                CellUnitsDataContainer.ShiftPlayerUnitToBaseCell(FromXy, ToXy);

                InfoUnitsDataContainer.AddUnitInCondition(ConditionUnitTypes.None, fromUnitType, fromIsMasterClient, ToXy);


                CellUnitsDataContainer.TakeAmountSteps(ToXy, CellEnvirDataContainer.NeedAmountSteps(ToXy));
                if (CellUnitsDataContainer.AmountSteps(ToXy) < 0) CellUnitsDataContainer.ResetAmountSteps(ToXy);

                CellUnitsDataContainer.ResetConditionType(ToXy);
            }
        }
    }
}
