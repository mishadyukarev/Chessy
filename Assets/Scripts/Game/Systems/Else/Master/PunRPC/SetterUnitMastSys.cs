using Leopotam.Ecs;
using Scripts.Common;
using System;

namespace Scripts.Game
{
    internal sealed class SetterUnitMastSys : IEcsRunSystem
    {
        private EcsFilter<ForSettingUnitMasCom> _setterFilter = default;
        private EcsFilter<CellsForSetUnitComp> _cellsSetUnitFilter = default;

        private EcsFilter<CellEnvironmentDataC> _cellEnvirDataFilter = default;
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;

        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);
            var unitTypeForSet = _setterFilter.Get1(0).UnitTypeForSetting;
            var idxForSet = _setterFilter.Get1(0).IdxCellForSetting;

            ref var curEnvDatCom = ref _cellEnvirDataFilter.Get1(idxForSet);
            ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxForSet);
            ref var curOwnUnitCom = ref _cellUnitFilter.Get2(idxForSet);


            var playerSend = WhoseMoveC.WhoseMove;


            if (_cellsSetUnitFilter.Get1(0).HaveIdxCell(playerSend, idxForSet))
            {
                int newAmountHealth;
                int newAmountSteps;

                newAmountSteps = UnitValues.StandartAmountSteps(unitTypeForSet);
                curUnitDatCom.LevelUnitType = LevelUnitTypes.Wood;
                newAmountHealth = UnitValues.StandAmountHealthAll;

                curUnitDatCom.UnitType = unitTypeForSet;
                curUnitDatCom.AmountHealth = newAmountHealth;
                curUnitDatCom.AmountSteps = newAmountSteps;
                curUnitDatCom.CondUnitType = default;
                curOwnUnitCom.PlayerType = playerSend;

                if(InventorUnitsC.HaveUnitInInv(playerSend, unitTypeForSet, LevelUnitTypes.Iron))
                {
                    InventorUnitsC.TakeUnitsInInv(playerSend, unitTypeForSet, LevelUnitTypes.Iron);
                    curUnitDatCom.LevelUnitType = LevelUnitTypes.Iron;
                }
                else
                {
                    InventorUnitsC.TakeUnitsInInv(playerSend, unitTypeForSet, LevelUnitTypes.Wood);
                    curUnitDatCom.LevelUnitType = LevelUnitTypes.Wood;
                }

                RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);

                
            }
        }
    }
}