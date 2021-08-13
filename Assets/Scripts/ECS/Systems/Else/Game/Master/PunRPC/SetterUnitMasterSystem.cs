using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Game.Master;
using Leopotam.Ecs;
using System;

internal sealed class SetterUnitMasterSystem : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _infoFilter = default;

    private EcsFilter<ForSettingUnitMasCom> _setterFilter = default;
    private EcsFilter<InventorUnitsComponent> _unitInventorFilter = default;
    private EcsFilter<UnitsInGameInfoComponent> _idxUnitsFilter = default;
    private EcsFilter<UnitsInConditionInGameCom> _idxUnitsInCondFilter = default;

    private EcsFilter<CellEnvironDataCom> _cellEnvirDataFilter = default;
    private EcsFilter<CellUnitDataComponent, OwnerComponent> _cellUnitFilter = default;
    private EcsFilter<CellDataComponent> _cellDataFilter = default;

    public void Run()
    {
        var sender = _infoFilter.Get1(0).FromInfo.Sender;
        var unitTypeForSetting = _setterFilter.Get1(0).UnitTypeForSetting;
        var idxCellForSetting = _setterFilter.Get1(0).IdxCellForSetting;

        ref var unitInventrorCom = ref _unitInventorFilter.Get1(0);
        ref var unitsInGameCom = ref _idxUnitsFilter.Get1(0);

        ref var cellEnvrDataCom = ref _cellEnvirDataFilter.Get1(idxCellForSetting);
        ref var cellUnitDataCom = ref _cellUnitFilter.Get1(idxCellForSetting);
        ref var ownerCellCom = ref _cellUnitFilter.Get2(idxCellForSetting);


        if (!cellEnvrDataCom.HaveEnvironment(EnvironmentTypes.Mountain)
            && !cellUnitDataCom.HaveUnit
            && _cellDataFilter.Get1(idxCellForSetting).IsStartedCell(sender.IsMasterClient))
        {
            int newAmountHealth;
            int newAmountSteps;

            switch (unitTypeForSetting)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    newAmountHealth = UnitValues.STANDART_AMOUNT_HEALTH_KING;
                    newAmountSteps = UnitValues.STANDART_AMOUNT_STEPS_KING;
                    break;

                case UnitTypes.Pawn_Axe:
                    newAmountHealth = UnitValues.STANDART_AMOUNT_HEALTH_PAWN_AXE;
                    newAmountSteps = UnitValues.STANDART_AMOUNT_STEPS_PAWN;
                    break;

                case UnitTypes.Rook_Bow:
                    newAmountHealth = UnitValues.STANDART_AMOUNT_HEALTH_ROOK_BOW;
                    newAmountSteps = UnitValues.STANDART_AMOUNT_STEPS_ROOK_BOW;
                    break;

                case UnitTypes.Bishop_Bow:
                    newAmountHealth = UnitValues.STANDART_AMOUNT_HEALTH_BISHOP_BOW;
                    newAmountSteps = UnitValues.STANDART_AMOUNT_STEPS_BISHOP_BOW;
                    break;

                default:
                    throw new Exception();
            }

            cellUnitDataCom.UnitType = unitTypeForSetting;
            cellUnitDataCom.AmountHealth = newAmountHealth;
            cellUnitDataCom.AmountSteps = newAmountSteps;
            cellUnitDataCom.ConditionType = default;
            ownerCellCom.SetOwner(sender);


            unitInventrorCom.TakeUnitsInInventor(unitTypeForSetting, sender.IsMasterClient);
            unitsInGameCom.AddAmountUnitInGame(unitTypeForSetting, sender.IsMasterClient, idxCellForSetting);
            _idxUnitsInCondFilter.Get1(0).AddUnitInCondition(ConditionUnitTypes.None, unitTypeForSetting, sender.IsMasterClient, idxCellForSetting);


            RPCGameSystem.SetUnitToGeneral(sender, true);
            RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);
        }

        else
        {
            RPCGameSystem.SetUnitToGeneral(sender, false);
        }
    }
}
