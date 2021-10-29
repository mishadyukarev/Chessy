using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class SetterUnitMastSys : IEcsRunSystem
    {
        private EcsFilter<ForSettingUnitMasCom> _setterFilter = default;
        private EcsFilter<CellsForSetUnitComp> _cellsSetUnitFilter = default;

        private EcsFilter<CellEnvironmentDataC> _cellEnvirDataFilter = default;
        private EcsFilter<CellUnitDataCom, HpComponent, StepComponent, ToolWeaponC, OwnerCom> _cellUnitFilter = default;

        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);
            var unitTypeForSet = _setterFilter.Get1(0).UnitTypeForSetting;
            var idxForSet = _setterFilter.Get1(0).IdxCellForSetting;

            ref var curEnvDatCom = ref _cellEnvirDataFilter.Get1(idxForSet);

            ref var curUnitC = ref _cellUnitFilter.Get1(idxForSet);
            ref var curHpUnitC = ref _cellUnitFilter.Get2(idxForSet);
            ref var stepUnitC = ref _cellUnitFilter.Get3(idxForSet);
            ref var curTwUnitC = ref _cellUnitFilter.Get4(idxForSet);
            ref var curOwnUnitC = ref _cellUnitFilter.Get5(idxForSet);


            var playerSend = WhoseMoveC.WhoseMove;


            if (_cellsSetUnitFilter.Get1(0).HaveIdxCell(playerSend, idxForSet))
            {
                curUnitC.UnitType = unitTypeForSet;
                curUnitC.LevelUnitType = LevelUnitTypes.Wood;
                curTwUnitC.TWExtraType = default;
                curHpUnitC.AmountHealth = UnitValues.StandAmountHealth(unitTypeForSet);
                stepUnitC.AmountSteps = UnitValues.StandartAmountSteps(unitTypeForSet);
                curUnitC.CondUnitType = default;
                curOwnUnitC.PlayerType = playerSend;

                if (InventorUnitsC.HaveUnitInInv(playerSend, unitTypeForSet, LevelUnitTypes.Iron))
                {
                    InventorUnitsC.TakeUnitsInInv(playerSend, unitTypeForSet, LevelUnitTypes.Iron);
                    curUnitC.LevelUnitType = LevelUnitTypes.Iron;
                }
                else
                {
                    InventorUnitsC.TakeUnitsInInv(playerSend, unitTypeForSet, LevelUnitTypes.Wood);
                    curUnitC.LevelUnitType = LevelUnitTypes.Wood;
                }

                RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);


            }
        }
    }
}