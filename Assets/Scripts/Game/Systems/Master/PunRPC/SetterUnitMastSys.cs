using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class SetterUnitMastSys : IEcsRunSystem
    {
        private EcsFilter<ForSettingUnitMasCom> _setterFilter = default;
        private EcsFilter<CellsForSetUnitComp> _cellsSetUnitFilter = default;

        private EcsFilter<CellEnvironmentDataC> _cellEnvirDataFilter = default;
        private EcsFilter<CellUnitDataCom, HpUnitC, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, UnitEffectsC, ThirstyC, OwnerCom> _cellUnitOthFilt = default;

        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);
            var unitTypeForSet = _setterFilter.Get1(0).UnitTypeForSetting;
            var idxForSet = _setterFilter.Get1(0).IdxCellForSetting;

            ref var curEnvDatCom = ref _cellEnvirDataFilter.Get1(idxForSet);

            ref var curUnitC = ref _cellUnitFilter.Get1(idxForSet);
            ref var curHpUnitC = ref _cellUnitFilter.Get2(idxForSet);
            ref var stepUnitC = ref _cellUnitFilter.Get3(idxForSet);

            ref var condUnitC = ref _cellUnitOthFilt.Get2(idxForSet);
            ref var curTwUnitC = ref _cellUnitOthFilt.Get3(idxForSet);
            ref var curEffUnitC = ref _cellUnitOthFilt.Get4(idxForSet);
            ref var thirUnitC_0 =ref _cellUnitOthFilt.Get5(idxForSet);
            ref var curOwnUnitC = ref _cellUnitOthFilt.Get6(idxForSet);


            var playerSend = WhoseMoveC.WhoseMove;


            if (_cellsSetUnitFilter.Get1(0).HaveIdxCell(playerSend, idxForSet))
            {
                curUnitC.UnitType = unitTypeForSet;
                curUnitC.LevelUnitType = LevelUnitTypes.Wood;
                curTwUnitC.ToolWeapType = default;
                curHpUnitC.AmountHp = curHpUnitC.MaxHpUnit(curEffUnitC, unitTypeForSet);
                stepUnitC.AmountSteps = UnitValues.StandartAmountSteps(false, unitTypeForSet);
                condUnitC.DefCondition();
                curOwnUnitC.PlayerType = playerSend;
                thirUnitC_0.SetMaxWater(unitTypeForSet);

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