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

                newAmountSteps = UnitValues.StandartAmountSteps(unitTypeForSet);
                curUnitDatCom.LevelUnitType = LevelUnitTypes.Wood;
                newAmountHealth = UnitValues.StandartAmountHealth(unitTypeForSet, curUnitDatCom.LevelUnitType);

                curUnitDatCom.UnitType = unitTypeForSet;
                curUnitDatCom.AmountHealth = newAmountHealth;
                curUnitDatCom.AmountSteps = newAmountSteps;
                curUnitDatCom.CondUnitType = default;
                curOwnUnitCom.PlayerType = playerSender;

                if(unitInvCom.HaveUnitInInv(playerSender, unitTypeForSet, LevelUnitTypes.Iron))
                {
                    unitInvCom.TakeUnitsInInv(playerSender, unitTypeForSet, LevelUnitTypes.Iron);
                    curUnitDatCom.LevelUnitType = LevelUnitTypes.Iron;
                }
                else
                {
                    unitInvCom.TakeUnitsInInv(playerSender, unitTypeForSet, LevelUnitTypes.Wood);
                    curUnitDatCom.LevelUnitType = LevelUnitTypes.Wood;
                }

                RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);

                
            }
        }
    }
}