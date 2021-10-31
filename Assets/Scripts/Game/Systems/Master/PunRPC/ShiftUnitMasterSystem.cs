using Leopotam.Ecs;
using Photon.Pun;

namespace Scripts.Game
{
    internal sealed class ShiftUnitMasterSystem : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, LevelUnitC, OwnerCom> _cellUnitMainFilt = default;
        private EcsFilter<CellUnitDataCom, HpUnitC, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, UnitEffectsC, ThirstyUnitC> _cellUnitOthFilt = default;
        private EcsFilter<CellEnvDataC> _cellEnvrDataFilter = default;
        private EcsFilter<CellRiverDataC> _cellRiverFilt = default;


        public void Run()
        {
            var fromIdx = ForShiftMasCom.IdxFrom;
            var toIdx = ForShiftMasCom.IdxTo;

            var playerType = WhoseMoveC.WhoseMove;


            if (CellsForShiftCom.HaveIdxCell(playerType, fromIdx, toIdx))
            {
                ref var unitC_from = ref _cellUnitFilter.Get1(fromIdx);
                ref var levUnitC_from = ref _cellUnitMainFilt.Get2(fromIdx);
                ref var ownUnitC_from = ref _cellUnitMainFilt.Get3(fromIdx);
                ref var hpUnitC_from = ref _cellUnitFilter.Get2(fromIdx);
                ref var stepUnitC_from = ref _cellUnitFilter.Get3(fromIdx);
                ref var twUnitC_from = ref _cellUnitOthFilt.Get3(fromIdx);
                ref var effUnitC_from = ref _cellUnitOthFilt.Get4(fromIdx);
                ref var thirUnitC_from = ref _cellUnitOthFilt.Get5(fromIdx);

                ref var unitDatC_to = ref _cellUnitFilter.Get1(toIdx);
                ref var levUnitC_to = ref _cellUnitMainFilt.Get2(toIdx);
                ref var ownUnitC_to = ref _cellUnitMainFilt.Get3(toIdx);
                ref var hpUnitC_to = ref _cellUnitFilter.Get2(toIdx);
                ref var stepUnitC_to = ref _cellUnitFilter.Get3(toIdx);
                ref var condUnitC_to  = ref _cellUnitOthFilt.Get2(toIdx);
                ref var twUnitC_to = ref _cellUnitOthFilt.Get3(toIdx);
                ref var effUnitC_to = ref _cellUnitOthFilt.Get4(toIdx);
                ref var thirUnitC_to = ref _cellUnitOthFilt.Get5(toIdx);

                ref var envDatC_To = ref _cellEnvrDataFilter.Get1(toIdx);

                ref var riverC_to = ref _cellRiverFilt.Get1(toIdx);


                stepUnitC_from.TakeStepsForDoing(envDatC_To);
                //effUnitC_from.Def(StatTypes.Steps);


                unitDatC_to.SetUnit(unitC_from.UnitType);
                levUnitC_to.SetNewLevel(levUnitC_from.LevelUnitType);
                hpUnitC_to.AmountHp = hpUnitC_from.AmountHp;
                stepUnitC_to.StepsAmount = stepUnitC_from.StepsAmount;
                condUnitC_to.DefCondition();
                twUnitC_to.Set(twUnitC_from);
                effUnitC_to.Set(effUnitC_from);
                ownUnitC_to.SetOwner(playerType);
                thirUnitC_to.Set(thirUnitC_from);

                unitC_from.NoneUnit();
                levUnitC_from.SetNewLevel(LevelUnitTypes.None);

                if (riverC_to.HaveNearRiver) thirUnitC_to.SetMaxWater(unitDatC_to.UnitType);


                RpcSys.SoundToGeneral(InfoC.Sender(MasGenOthTypes.Master), SoundEffectTypes.ClickToTable);
            }
        }
    }
}