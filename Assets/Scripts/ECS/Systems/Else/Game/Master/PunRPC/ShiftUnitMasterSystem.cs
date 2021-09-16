using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Leopotam.Ecs;

internal sealed class ShiftUnitMasterSystem : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _infoFilter = default;
    private EcsFilter<ForShiftMasCom> _forShiftFilter = default;

    private EcsFilter<AvailCellsForShiftComp> _availCellsForShiftFilter = default;

    private EcsFilter<CellUnitDataComponent, OwnerOnlineComp> _cellUnitFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvrDataFilter = default;


    public void Run()
    {
        var fromInfo = _infoFilter.Get1(0).FromInfo;

        var fromIdx = _forShiftFilter.Get1(0).IdxFrom;
        var toIdx = _forShiftFilter.Get1(0).IdxTo;

        if (_availCellsForShiftFilter.Get1(0).HaveIdxCell(fromInfo.Sender.IsMasterClient, fromIdx, toIdx))
        {
            ref var forShiftMasCom = ref _forShiftFilter.Get1(0);

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


                    toCellUnitDataCom.ArcherWeaponType = fromCellUnitDataCom.ArcherWeaponType;
                    toCellUnitDataCom.ExtraTWPawnType = fromCellUnitDataCom.ExtraTWPawnType;
                    toCellUnitDataCom.UnitType = fromCellUnitDataCom.UnitType;
                    toCellUnitDataCom.AmountHealth = fromCellUnitDataCom.AmountHealth;
                    toCellUnitDataCom.AmountSteps = fromCellUnitDataCom.AmountSteps;
                    toCellUnitDataCom.ConditionUnitType = default;
                    toOwnerCellUnitCom.SetOwner(fromInfo.Sender);


                    fromCellUnitDataCom.ResetUnit();
                    fromOwnerCellUnitCom.ResetOwner();

                    RpcSys.SoundToGeneral(fromInfo.Sender, SoundEffectTypes.ClickToTable);
                }
            }
        }
    }
}
