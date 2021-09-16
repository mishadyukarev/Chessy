using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Leopotam.Ecs;
using System;

internal sealed class SetterUnitMasterSystem : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _infoFilter = default;

    private EcsFilter<ForSettingUnitMasCom> _setterFilter = default;
    private EcsFilter<InventorUnitsComponent> _unitInventorFilter = default;
    private EcsFilter<CellsForSetUnitComp> _availCellsForSetUnitFilter = default;

    private EcsFilter<CellEnvironDataCom> _cellEnvirDataFilter = default;
    private EcsFilter<CellUnitDataComponent, OwnerOnlineComp> _cellUnitFilter = default;

    public void Run()
    {
        var sender = _infoFilter.Get1(0).FromInfo.Sender;
        var unitTypeForSetting = _setterFilter.Get1(0).UnitTypeForSetting;
        var idxCellForSetting = _setterFilter.Get1(0).IdxCellForSetting;

        ref var unitInventrorCom = ref _unitInventorFilter.Get1(0);

        ref var curCellEnvrDataCom = ref _cellEnvirDataFilter.Get1(idxCellForSetting);
        ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(idxCellForSetting);
        ref var ownerCellCom = ref _cellUnitFilter.Get2(idxCellForSetting);


        if (_availCellsForSetUnitFilter.Get1(0).HaveIdxCell(sender.IsMasterClient, idxCellForSetting))
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
                    curCellUnitDataCom.ArcherWeaponType = default;
                    break;

                case UnitTypes.Pawn:
                    newAmountHealth = UnitValues.STANDART_AMOUNT_HEALTH_PAWN;
                    newAmountSteps = UnitValues.STANDART_AMOUNT_STEPS_PAWN;
                    break;

                case UnitTypes.Rook:
                    newAmountHealth = UnitValues.STANDART_AMOUNT_HEALTH_ROOK;
                    newAmountSteps = UnitValues.STANDART_AMOUNT_STEPS_ROOK;
                    curCellUnitDataCom.ArcherWeaponType = ToolWeaponTypes.Bow;
                    break;

                case UnitTypes.Bishop:
                    newAmountHealth = UnitValues.STANDART_AMOUNT_HEALTH_BISHOP;
                    newAmountSteps = UnitValues.STANDART_AMOUNT_STEPS_BISHOP;
                    curCellUnitDataCom.ArcherWeaponType = ToolWeaponTypes.Bow;
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

            RpcSys.SetUnitToGeneral(sender, true);
            RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);
        }
        else
        {
            RpcSys.SetUnitToGeneral(sender, false);
        }

    }
}
