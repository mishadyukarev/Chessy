using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Leopotam.Ecs;
using System;

internal sealed class SetterUnitMasterSystem : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _infoFilter = default;

    private EcsFilter<WhoseMoveComp> _whoseMoveFilter = default;

    private EcsFilter<ForSettingUnitMasCom> _setterFilter = default;
    private EcsFilter<InventorUnitsComponent> _unitInventorFilter = default;
    private EcsFilter<CellsForSetUnitComp> _cellsSetUnitFilter = default;

    private EcsFilter<CellEnvironDataCom> _cellEnvirDataFilter = default;
    private EcsFilter<CellUnitDataComponent, OwnerOnlineComp> _cellUnitFilter = default;


    public void Run()
    {
        var sender = _infoFilter.Get1(0).FromInfo.Sender;
        var unitTypeForSet = _setterFilter.Get1(0).UnitTypeForSetting;
        var idxForSet = _setterFilter.Get1(0).IdxCellForSetting;

        ref var unitInvCom = ref _unitInventorFilter.Get1(0);

        ref var curEnvDatCom = ref _cellEnvirDataFilter.Get1(idxForSet);
        ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxForSet);
        ref var curOwnUnitCom = ref _cellUnitFilter.Get2(idxForSet);


        bool isMaster = default;

        if (GameModeTypeComp.IsGameModeType(GameModeTypes.FriendOff))
        {
            isMaster = _whoseMoveFilter.Get1(0).IsMainMove;
        }
        else if(GameModeTypeComp.IsGameModeType(GameModeTypes.TrainingOff))
        {
            isMaster = true;
        }
        else
        {
            isMaster = sender.IsMasterClient;
        }


        if (_cellsSetUnitFilter.Get1(0).HaveIdxCell(isMaster, idxForSet))
        {
            int newAmountHealth;
            int newAmountSteps;

            switch (unitTypeForSet)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    newAmountHealth = UnitValues.STANDART_AMOUNT_HEALTH_KING;
                    newAmountSteps = UnitValues.STANDART_AMOUNT_STEPS_KING;
                    curUnitDatCom.ArcherWeaponType = default;
                    break;

                case UnitTypes.Pawn:
                    newAmountHealth = UnitValues.STANDART_AMOUNT_HEALTH_PAWN;
                    newAmountSteps = UnitValues.STANDART_AMOUNT_STEPS_PAWN;
                    break;

                case UnitTypes.Rook:
                    newAmountHealth = UnitValues.STANDART_AMOUNT_HEALTH_ROOK;
                    newAmountSteps = UnitValues.STANDART_AMOUNT_STEPS_ROOK;
                    curUnitDatCom.ArcherWeaponType = ToolWeaponTypes.Bow;
                    break;

                case UnitTypes.Bishop:
                    newAmountHealth = UnitValues.STANDART_AMOUNT_HEALTH_BISHOP;
                    newAmountSteps = UnitValues.STANDART_AMOUNT_STEPS_BISHOP;
                    curUnitDatCom.ArcherWeaponType = ToolWeaponTypes.Bow;
                    break;

                default:
                    throw new Exception();
            }

            curUnitDatCom.UnitType = unitTypeForSet;
            curUnitDatCom.AmountHealth = newAmountHealth;
            curUnitDatCom.AmountSteps = newAmountSteps;
            curUnitDatCom.ConditionUnitType = default;
            curOwnUnitCom.SetOwner(sender);


            unitInvCom.TakeUnitsInInventor(unitTypeForSet, sender.IsMasterClient);

            GameGeneralSysManager.RpcGameSys.SetUnitToGeneral(sender, true);


            RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);
        }
        else
        {
            GameGeneralSysManager.RpcGameSys.SetUnitToGeneral(sender, false);
        }

    }
}
