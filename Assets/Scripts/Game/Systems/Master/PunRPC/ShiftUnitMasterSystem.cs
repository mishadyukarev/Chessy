using Leopotam.Ecs;
using Photon.Pun;

namespace Scripts.Game
{
    internal sealed class ShiftUnitMasterSystem : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, HpUnitC, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, UnitEffectsC, ThirstyC, OwnerCom> _cellUnitOthFilt = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvrDataFilter = default;
        private EcsFilter<CellRiverDataC> _cellRiverFilt = default;


        public void Run()
        {
            var fromIdx = ForShiftMasCom.IdxFrom;
            var toIdx = ForShiftMasCom.IdxTo;

            var playerType = WhoseMoveC.WhoseMove;


            if (CellsForShiftCom.HaveIdxCell(playerType, fromIdx, toIdx))
            {
                ref var unitDatC_from = ref _cellUnitFilter.Get1(fromIdx);
                ref var hpUnitC_from = ref _cellUnitFilter.Get2(fromIdx);
                ref var stepUnitC_from = ref _cellUnitFilter.Get3(fromIdx);
                ref var twUnitC_from = ref _cellUnitOthFilt.Get3(fromIdx);
                ref var effUnitC_from = ref _cellUnitOthFilt.Get4(fromIdx);
                ref var thirUnitC_from = ref _cellUnitOthFilt.Get5(fromIdx);
                ref var ownUnitC_from = ref _cellUnitOthFilt.Get6(fromIdx);

                ref var unitDatC_to = ref _cellUnitFilter.Get1(toIdx);
                ref var hpUnitC_to = ref _cellUnitFilter.Get2(toIdx);
                ref var stepUnitC_to = ref _cellUnitFilter.Get3(toIdx);
                ref var condUnitC_to  = ref _cellUnitOthFilt.Get2(toIdx);
                ref var twUnitC_to = ref _cellUnitOthFilt.Get3(toIdx);
                ref var effUnitC_to = ref _cellUnitOthFilt.Get4(toIdx);
                ref var thirUnitC_to = ref _cellUnitOthFilt.Get5(toIdx);
                ref var ownUnitC_to = ref _cellUnitOthFilt.Get6(toIdx);

                ref var envDatC_To = ref _cellEnvrDataFilter.Get1(toIdx);

                ref var riverC_to = ref _cellRiverFilt.Get1(toIdx);


                stepUnitC_from.TakeSteps(envDatC_To.NeedAmountSteps);
                //effUnitC_from.Def(StatTypes.Steps);


                unitDatC_to.Set(unitDatC_from);
                hpUnitC_to.AmountHp = hpUnitC_from.AmountHp;
                stepUnitC_to.AmountSteps = stepUnitC_from.AmountSteps;
                condUnitC_to.DefCondition();
                twUnitC_to.Set(twUnitC_from);
                effUnitC_to.Set(effUnitC_from);
                ownUnitC_to.PlayerType = playerType;
                thirUnitC_to.Set(thirUnitC_from);

                unitDatC_from.DefUnitType();

                if (riverC_to.HaveNearRiver) thirUnitC_to.SetMaxWater(unitDatC_to.UnitType);


                RpcSys.SoundToGeneral(InfoC.Sender(MasGenOthTypes.Master), SoundEffectTypes.ClickToTable);
            }
        }
    }
}