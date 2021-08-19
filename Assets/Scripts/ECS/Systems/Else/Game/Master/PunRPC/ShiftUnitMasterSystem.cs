using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Game.Master;
using Leopotam.Ecs;

internal sealed class ShiftUnitMasterSystem : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _infoFilter = default;

    private EcsFilter<ForShiftMasCom> _forShiftFilter = default;

    private EcsFilter<UnitsInConditionInGameCom> _idxUnitsInCondFilter = default;
    private EcsFilter<UnitsInGameInfoComponent> _idxUnitsFilter = default;

    private EcsFilter<CellUnitDataComponent, OwnerComponent> _cellUnitFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvrDataFilter = default;

    public void Run()
    {
        var fromInfo = _infoFilter.Get1(0).FromInfo;

        var fromIdx = _forShiftFilter.Get1(0).IdxFrom;
        var toIdx = _forShiftFilter.Get1(0).IdxTo;

        ref var forShiftMasCom = ref _forShiftFilter.Get1(0);
        ref var idxUnitsInCondCom = ref _idxUnitsInCondFilter.Get1(0);
        ref var idxUnitsCom = ref _idxUnitsFilter.Get1(0);

        ref var fromCellUnitDataCom = ref _cellUnitFilter.Get1(fromIdx);
        ref var fromOwnerCellUnitCom = ref _cellUnitFilter.Get2(fromIdx);

        ref var toCellUnitDataCom = ref _cellUnitFilter.Get1(toIdx);
        ref var toOwnerCellUnitCom = ref _cellUnitFilter.Get2(toIdx);

        ref var toCellEnvDataCom = ref _cellEnvrDataFilter.Get1(toIdx);


        if (fromOwnerCellUnitCom.IsHim(fromInfo.Sender) && fromCellUnitDataCom.HaveMinAmountSteps && !toCellUnitDataCom.HaveUnit)
        {
            var neededAmountStepsForShift = toCellEnvDataCom.NeedAmountSteps;

            if (fromCellUnitDataCom.AmountSteps >= neededAmountStepsForShift || fromCellUnitDataCom.HaveMaxAmountSteps)
            {
                fromCellUnitDataCom.TakeAmountSteps(neededAmountStepsForShift);
                if (fromCellUnitDataCom.AmountSteps < 0) fromCellUnitDataCom.ResetAmountSteps();


                idxUnitsInCondCom.RemoveUnitInCondition(fromCellUnitDataCom.ConditionUnitType, fromCellUnitDataCom.UnitType, fromOwnerCellUnitCom.IsMasterClient, fromIdx);
                idxUnitsInCondCom.AddUnitInCondition(ConditionUnitTypes.None, fromCellUnitDataCom.UnitType, fromOwnerCellUnitCom.IsMasterClient, toIdx);

                idxUnitsCom.RemoveAmountUnitsInGame(fromCellUnitDataCom.UnitType, fromOwnerCellUnitCom.IsMasterClient, fromIdx);
                idxUnitsCom.AddAmountUnitInGame(fromCellUnitDataCom.UnitType, fromOwnerCellUnitCom.IsMasterClient, toIdx);


                toCellUnitDataCom.MainToolWeaponType = fromCellUnitDataCom.MainToolWeaponType;
                toCellUnitDataCom.ExtraToolWeaponType = fromCellUnitDataCom.ExtraToolWeaponType;
                toCellUnitDataCom.UnitType = fromCellUnitDataCom.UnitType;
                toCellUnitDataCom.AmountHealth = fromCellUnitDataCom.AmountHealth;
                toCellUnitDataCom.AmountSteps = fromCellUnitDataCom.AmountSteps;
                toCellUnitDataCom.ConditionUnitType = default;
                toOwnerCellUnitCom.SetOwner(fromInfo.Sender);




                fromCellUnitDataCom.ResetUnit();
                fromOwnerCellUnitCom.ResetOwner();

                RpcGameSystem.SoundToGeneral(fromInfo.Sender, SoundEffectTypes.ClickToTable);
            }
        }
    }
}
