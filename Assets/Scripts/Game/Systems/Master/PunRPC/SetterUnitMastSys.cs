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
            var idx_0 = _setterFilter.Get1(0).IdxCellForSetting;

            ref var curEnvDatCom = ref _cellEnvirDataFilter.Get1(idx_0);

            ref var unitC_0 = ref _cellUnitFilter.Get1(idx_0);

            ref var levUnitC_0 = ref _cellUnitMainFilt.Get2(idx_0);
            ref var ownUnitC_0 = ref _cellUnitMainFilt.Get3(idx_0);

            ref var curHpUnitC = ref _cellUnitFilter.Get2(idx_0);
            ref var stepUnitC = ref _cellUnitFilter.Get3(idx_0);

            ref var condUnitC = ref _cellUnitOthFilt.Get2(idx_0);
            ref var curTwUnitC = ref _cellUnitOthFilt.Get3(idx_0);
            ref var curEffUnitC = ref _cellUnitOthFilt.Get4(idx_0);
            ref var thirUnitC_0 =ref _cellUnitOthFilt.Get5(idx_0);


            var playerSend = WhoseMoveC.WhoseMove;


            if (_cellsSetUnitFilter.Get1(0).HaveIdxCell(playerSend, idx_0))
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
                    levUnitC_0.SetLevel(LevelUnitTypes.Iron);
                }
                else
                {
                    InventorUnitsC.TakeUnitsInInv(playerSend, unitTypeForSet, LevelUnitTypes.Wood);
                    levUnitC_0.SetLevel(LevelUnitTypes.Wood);
                }
                WhereUnitsC.Add(ownUnitC_0.Owner, unitC_0.Unit, levUnitC_0.Level, idx_0);

                RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);
            }
        }
    }
}