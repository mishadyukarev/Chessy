using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class SetterUnitMastSys : IEcsRunSystem
    {
        private EcsFilter<ForSettingUnitMasCom> _setterFilter = default;
        private EcsFilter<CellsForSetUnitComp> _cellsSetUnitFilter = default;

        private EcsFilter<CellEnvDataC> _cellEnvirDataFilter = default;

        private EcsFilter<CellUnitDataCom, LevelUnitC, OwnerCom> _cellUnitMainFilt = default;
        private EcsFilter<CellUnitDataCom, HpUnitC, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, UnitEffectsC, ThirstyUnitC> _cellUnitOthFilt = default;

        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);
            var unitTypeForSet = _setterFilter.Get1(0).UnitTypeForSetting;
            var idxForSet = _setterFilter.Get1(0).IdxCellForSetting;

            ref var curEnvDatCom = ref _cellEnvirDataFilter.Get1(idxForSet);

            ref var unitC_0 = ref _cellUnitFilter.Get1(idxForSet);

            ref var levUnitC_0 = ref _cellUnitMainFilt.Get2(idxForSet);
            ref var ownUnitC_0 = ref _cellUnitMainFilt.Get3(idxForSet);

            ref var curHpUnitC = ref _cellUnitFilter.Get2(idxForSet);
            ref var stepUnitC = ref _cellUnitFilter.Get3(idxForSet);

            ref var condUnitC = ref _cellUnitOthFilt.Get2(idxForSet);
            ref var curTwUnitC = ref _cellUnitOthFilt.Get3(idxForSet);
            ref var curEffUnitC = ref _cellUnitOthFilt.Get4(idxForSet);
            ref var thirUnitC_0 =ref _cellUnitOthFilt.Get5(idxForSet);


            var playerSend = WhoseMoveC.WhoseMove;


            if (_cellsSetUnitFilter.Get1(0).HaveIdxCell(playerSend, idxForSet))
            {
                unitC_0.SetUnit(unitTypeForSet);
                ownUnitC_0.SetOwner(playerSend);
                curTwUnitC.ToolWeapType = default;
                curHpUnitC.AmountHp = curHpUnitC.MaxHpUnit(curEffUnitC, unitTypeForSet);
                stepUnitC.StepsAmount = UnitValues.StandartAmountSteps(false, unitTypeForSet);
                condUnitC.DefCondition();
                thirUnitC_0.SetMaxWater(unitTypeForSet);

                if (InventorUnitsC.HaveUnitInInv(playerSend, unitTypeForSet, LevelUnitTypes.Iron))
                {
                    InventorUnitsC.TakeUnitsInInv(playerSend, unitTypeForSet, LevelUnitTypes.Iron);
                    levUnitC_0.SetNewLevel(LevelUnitTypes.Iron);
                }
                else
                {
                    InventorUnitsC.TakeUnitsInInv(playerSend, unitTypeForSet, LevelUnitTypes.Wood);
                    levUnitC_0.SetNewLevel(LevelUnitTypes.Wood);
                }

                RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);


            }
        }
    }
}