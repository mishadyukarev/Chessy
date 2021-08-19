using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
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

        ref var curCellEnvrDataCom = ref _cellEnvirDataFilter.Get1(idxCellForSetting);
        ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(idxCellForSetting);
        ref var ownerCellCom = ref _cellUnitFilter.Get2(idxCellForSetting);


        if (!curCellEnvrDataCom.HaveEnvironment(EnvironmentTypes.Mountain)
            && !curCellUnitDataCom.HaveUnit
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
                    curCellUnitDataCom.MainToolWeaponType = default;
                    break;

                case UnitTypes.Pawn:
                    newAmountHealth = UnitValues.STANDART_AMOUNT_HEALTH_PAWN_AXE;
                    newAmountSteps = UnitValues.STANDART_AMOUNT_STEPS_PAWN;
                    curCellUnitDataCom.MainToolWeaponType = ToolWeaponTypes.Axe;
                    break;

                case UnitTypes.Rook:
                    newAmountHealth = UnitValues.STANDART_AMOUNT_HEALTH_ROOK_BOW;
                    newAmountSteps = UnitValues.STANDART_AMOUNT_STEPS_ROOK_BOW;
                    curCellUnitDataCom.MainToolWeaponType = ToolWeaponTypes.Bow;
                    break;

                case UnitTypes.Bishop:
                    newAmountHealth = UnitValues.STANDART_AMOUNT_HEALTH_BISHOP_BOW;
                    newAmountSteps = UnitValues.STANDART_AMOUNT_STEPS_BISHOP_BOW;
                    curCellUnitDataCom.MainToolWeaponType = ToolWeaponTypes.Bow;
                    break;

                default:
                    throw new Exception();
            }

            curCellUnitDataCom.UnitType = unitTypeForSetting;
            curCellUnitDataCom.AmountHealth = newAmountHealth;
            curCellUnitDataCom.AmountSteps = newAmountSteps;
            curCellUnitDataCom.ConditionUnitType = default;
            ownerCellCom.SetOwner(sender);


            unitInventrorCom.TakeUnitsInInventor(unitTypeForSetting, sender.IsMasterClient);
            unitsInGameCom.AddAmountUnitInGame(unitTypeForSetting, sender.IsMasterClient, idxCellForSetting);
            _idxUnitsInCondFilter.Get1(0).AddUnitInCondition(ConditionUnitTypes.None, unitTypeForSetting, sender.IsMasterClient, idxCellForSetting);


            RpcGameSystem.SetUnitToGeneral(sender, true);
            RpcGameSystem.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);
        }

        else
        {
            RpcGameSystem.SetUnitToGeneral(sender, false);
        }
    }
}
