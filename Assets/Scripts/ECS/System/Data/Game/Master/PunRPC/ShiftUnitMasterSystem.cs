using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
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

        List<int[]> xyAvailableCellsForShift = CellUnitsDataSystem.GetCellsForShift(FromXy);

        if (CellUnitsDataSystem.IsHim(RpcMasterDataContainer.InfoFrom.Sender, FromXy) && CellUnitsDataSystem.HaveMinAmountSteps(FromXy))
        {
            if (xyAvailableCellsForShift.TryFindCell(ToXy))
            {
                PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.ClickToTable);


                var fromUnitType = CellUnitsDataSystem.UnitType(FromXy);
                var fromIsMasterClient = CellUnitsDataSystem.IsMasterClient(FromXy);

                var fromCondition = CellUnitsDataSystem.ConditionType(FromXy);


                InfoUnitsDataContainer.RemoveUnitInCondition(fromCondition, fromUnitType, fromIsMasterClient, FromXy);

                InfoUnitsDataContainer.RemoveAmountUnitsInGame(CellUnitsDataSystem.UnitType(FromXy), CellUnitsDataSystem.IsMasterClient(FromXy), FromXy);
                InfoUnitsDataContainer.AddAmountUnitInGame(CellUnitsDataSystem.UnitType(FromXy), CellUnitsDataSystem.IsMasterClient(FromXy), ToXy);
                CellUnitsDataSystem.ShiftPlayerUnitToBaseCell(FromXy, ToXy);

                InfoUnitsDataContainer.AddUnitInCondition(ConditionUnitTypes.None, fromUnitType, fromIsMasterClient, ToXy);


                CellUnitsDataSystem.TakeAmountSteps(ToXy, CellEnvrDataSystem.NeedAmountSteps(ToXy));
                if (CellUnitsDataSystem.AmountSteps(ToXy) < 0) CellUnitsDataSystem.ResetAmountSteps(ToXy);

                CellUnitsDataSystem.ResetConditionType(ToXy);
            }
        }
    }
}
