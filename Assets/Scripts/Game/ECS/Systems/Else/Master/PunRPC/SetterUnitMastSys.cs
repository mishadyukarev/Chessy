using Leopotam.Ecs;
using Scripts.Common;
using System;

namespace Scripts.Game
{
    internal sealed class SetterUnitMastSys : IEcsRunSystem
    {
        private EcsFilter<InfoCom> _infoFilter = default;

        private EcsFilter<ForSettingUnitMasCom> _setterFilter = default;
        private EcsFilter<InventorUnitsComponent> _unitInventorFilter = default;
        private EcsFilter<CellsForSetUnitComp> _cellsSetUnitFilter = default;

        private EcsFilter<CellEnvironDataCom> _cellEnvirDataFilter = default;
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;

        private EcsFilter<SoundEffectsComp> _soundEffFilter = default;

        public void Run()
        {
            var sender = _infoFilter.Get1(0).FromInfo.Sender;
            var unitTypeForSet = _setterFilter.Get1(0).UnitTypeForSetting;
            var idxForSet = _setterFilter.Get1(0).IdxCellForSetting;

            ref var unitInvCom = ref _unitInventorFilter.Get1(0);

            ref var curEnvDatCom = ref _cellEnvirDataFilter.Get1(idxForSet);
            ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxForSet);
            ref var curOwnUnitCom = ref _cellUnitFilter.Get2(idxForSet);


            PlayerTypes playerSender = default;

            if (GameModesCom.IsOfflineMode) playerSender = WhoseMoveCom.WhoseMoveOffline;
            else playerSender = sender.GetPlayerType();


            if (_cellsSetUnitFilter.Get1(0).HaveIdxCell(playerSender, idxForSet))
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
                        curUnitDatCom.ArcherWeapType = default;
                        break;

                    case UnitTypes.Pawn:
                        newAmountHealth = UnitValues.STANDART_AMOUNT_HEALTH_PAWN;
                        newAmountSteps = UnitValues.STANDART_AMOUNT_STEPS_PAWN;
                        curUnitDatCom.ArcherWeapType = default;
                        break;

                    case UnitTypes.Rook:
                        newAmountHealth = UnitValues.STANDART_AMOUNT_HEALTH_ROOK;
                        newAmountSteps = UnitValues.STANDART_AMOUNT_STEPS_ROOK;
                        curUnitDatCom.ArcherWeapType = ToolWeaponTypes.Bow;
                        break;

                    case UnitTypes.Bishop:
                        newAmountHealth = UnitValues.STANDART_AMOUNT_HEALTH_BISHOP;
                        newAmountSteps = UnitValues.STANDART_AMOUNT_STEPS_BISHOP;
                        curUnitDatCom.ArcherWeapType = ToolWeaponTypes.Bow;
                        break;

                    default:
                        throw new Exception();
                }

                curUnitDatCom.UnitType = unitTypeForSet;
                curUnitDatCom.AmountHealth = newAmountHealth;
                curUnitDatCom.AmountSteps = newAmountSteps;
                curUnitDatCom.CondUnitType = default;
                curOwnUnitCom.PlayerType = playerSender;


                RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);

                unitInvCom.TakeUnitsInInv(playerSender, unitTypeForSet);
            }
        }
    }
}