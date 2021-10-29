using Leopotam.Ecs;
using Photon.Pun;

namespace Scripts.Game
{
    internal sealed class ShiftUnitMasterSystem : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, HpUnitC, StepComponent, ConditionUnitC, UnitEffectsC, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvrDataFilter = default;

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
                ref var effUnitC_from = ref _cellUnitFilter.Get5(fromIdx);
                ref var OwnUnitC_from = ref _cellUnitFilter.Get6(fromIdx);

                ref var unitDatC_to = ref _cellUnitFilter.Get1(toIdx);
                ref var hpUnitC_to = ref _cellUnitFilter.Get2(toIdx);
                ref var stepUnitC_to = ref _cellUnitFilter.Get3(toIdx);
                ref var condUnitC_to  = ref _cellUnitFilter.Get4(toIdx);
                ref var effUnitC_to = ref _cellUnitFilter.Get5(toIdx);
                ref var ownUnitC_to = ref _cellUnitFilter.Get6(toIdx);

                ref var envDatC_To = ref _cellEnvrDataFilter.Get1(toIdx);



                stepUnitC_from.TakeSteps(envDatC_To.NeedAmountSteps);
                //effUnitC_from.Def(StatTypes.Steps);


                unitDatC_to.Set(unitDatC_from);
                hpUnitC_to.AmountHp = hpUnitC_from.AmountHp;
                stepUnitC_to.AmountSteps = stepUnitC_from.AmountSteps;
                condUnitC_to.DefCondition();
                effUnitC_to.Set(effUnitC_from);
                ownUnitC_to.PlayerType = playerType;

                unitDatC_from.DefUnitType();

                RpcSys.SoundToGeneral(InfoC.Sender(MasGenOthTypes.Master), SoundEffectTypes.ClickToTable);
            }
        }
    }
}